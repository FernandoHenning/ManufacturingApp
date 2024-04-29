using ManufacturingApp.DTOs.Requests.Recipe;
using ManufacturingApp.DTOs.Responses.Supplier;
using ManufacturingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ManufacturingApp.DataAccess.Repositories;

public class RecipeRepository(ManufacturingDbContext context) : IDisposable, IAsyncDisposable
{
    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await context.Recipes.AnyAsync(recipe => recipe.Id == id);
    }

    public async Task<IEnumerable<GetSupplier>> GetAllAsync()
    {
        return await context.Recipes
            .AsNoTracking()
            .Select(supplier =>
                new GetSupplier(supplier.Id, supplier.Name, supplier.Description))
            .ToListAsync();
    }

    public async Task<GetSupplier?> GetAsync(int supplierId)
    {
        var supplier = await context.Recipes.FindAsync(supplierId);
        return supplier == null
            ? null
            : new GetSupplier(supplier.Id, supplier.Name, supplier.Description);
    }

    public async Task CreateAsync(Recipe entity)
    {
        await context.AddAsync(entity);
    }

    public async Task AddRawMaterialAsync(RawMaterialRecipeInfo material, int recipeId)
    {
        await context.RecipeRawMaterials.AddAsync(new RecipeRawMaterial
        {
            RecipeId = recipeId,
            RawMaterialId = material.RawMaterialId,
            Quantity = material.Quantity
        });
    }

    public async Task AddProductAsync(ProductRecipeInfo product, int productId)
    {
        await context.RecipeProducts.AddAsync(new RecipeProduct
        {
            ProductId = product.ProductId,
            Quantity = product.Quantity,
            RecipeId = productId
        });
    }

    public async Task DeleteAsync(int supplierId)
    {
        var supplier = await context.Recipes.FindAsync(supplierId);
        context.Recipes.Remove(supplier);
    }

    public void Update(Recipe entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }


    public List<SupplierRawMaterialPrice> GetOptimalSuppliers(int recipeId)
    {
        var materials = context.Recipes
            .AsNoTracking()
            .Include(r => r.RecipeRawMaterials)
            .ThenInclude(rrm => rrm.RawMaterial)
            .SelectMany(rrm => rrm.RecipeRawMaterials.Select(recipeRawMaterial => recipeRawMaterial.RawMaterial))
            .ToList();

        var cheapestSuppliersQuery = context.SupplierRawMaterials
            .AsNoTracking()
            .Include(srm => srm.RawMaterial)
            .Include(srm => srm.Supplier)
            .Where(rm => materials.Any(m => m == rm.RawMaterial))
            .GroupBy(srm => srm.RawMaterial)
            .Select(g => new
            {
                RawMaterialId = g.Key.Id,
                CheapestSupplier = g.OrderBy(s => s.Price).FirstOrDefault()
            });
        
        var cheapestSuppliers = cheapestSuppliersQuery
            .AsEnumerable()
            .Select(result => new SupplierRawMaterialPrice
            (
                result.CheapestSupplier.Supplier.Name,
                result.CheapestSupplier.RawMaterial.Name,
                result.CheapestSupplier.Price
            ))
            .ToList();
        return cheapestSuppliers;
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            context.Dispose();
        }

        disposed = true;
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}