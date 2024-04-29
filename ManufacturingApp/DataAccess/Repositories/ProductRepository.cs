using ManufacturingApp.Models;
using Microsoft.EntityFrameworkCore;
using GetProduct = ManufacturingApp.DTOs.Responses.Product.GetProduct;

namespace ManufacturingApp.DataAccess.Repositories;

public class ProductRepository(ManufacturingDbContext context) : IDisposable, IAsyncDisposable
{
    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await context.Products.AnyAsync(product => product.Id == id);
    }
    public async Task<IEnumerable<GetProduct>> GetAllAsync()
    {
        return await context.Products
            .Select(product => new GetProduct(product.Id, product.Name, product.Description, product.SellingPrice))
            .ToListAsync();
    }

    public async Task<GetProduct?> GetAsync(int productId)
    {
        var product = await context.Products.FindAsync(productId);
        return product == null ? null : new GetProduct(product.Id, product.Name, product.Description, product.SellingPrice);
    }

    public async Task CreateAsync(Product entity)
    {
        await context.AddAsync(entity);
    }

    public async Task DeleteAsync(int productId)
    {
        var product = await context.Products.FindAsync(productId);
        context.Products.Remove(product);
    }

    public void Update(Product entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            context.Dispose();
        }

        _disposed = true;
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