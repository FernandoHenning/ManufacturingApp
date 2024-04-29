using ManufacturingApp.DTOs.Responses.Supplier;
using ManufacturingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ManufacturingApp.DataAccess.Repositories;

public class SupplierRepository(ManufacturingDbContext context) : IDisposable, IAsyncDisposable
{
    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await context.Suppliers.AnyAsync(supplier => supplier.Id == id);
    }
    public async Task<IEnumerable<GetSupplier>> GetAllAsync()
    {
        return await context.Suppliers
            .Select(supplier =>
                new GetSupplier(supplier.Id, supplier.Name, supplier.Description))
            .ToListAsync();
    }

    public async Task<GetSupplier?> GetAsync(int supplierId)
    {
        var suplier = await context.Suppliers.FindAsync(supplierId);
        return suplier == null
            ? null
            : new GetSupplier(suplier.Id, suplier.Name, suplier.Description);
    }

    public async Task CreateAsync(Supplier entity)
    {
        await context.AddAsync(entity);
    }

    public async Task AddRawMaterialToSupplierAsync(int rawMaterialId, int supplierId, decimal price)
    {
        await context.SupplierRawMaterials.AddAsync(new SupplierRawMaterial
        {
            RawMaterialId = rawMaterialId,
            SupplierId = supplierId,
            Price = price
        });
    }

    public async Task DeleteAsync(int supplierId)
    {
        var supplier = await context.Suppliers.FindAsync(supplierId);
        context.Suppliers.Remove(supplier);
    }

    public void Update(Supplier entity)
    {
        context.Entry(entity).State = EntityState.Modified;
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