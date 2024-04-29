using ManufacturingApp.DTOs.Responses.RawMaterial;
using ManufacturingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ManufacturingApp.DataAccess.Repositories;

public class RawMaterialRepository(ManufacturingDbContext context) : IDisposable, IAsyncDisposable
{
    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await context.RawMaterials.AnyAsync(material => material.Id == id);
    }
    public async Task<IEnumerable<GetRawMaterial>> GetAllAsync()
    {
        return await context.RawMaterials
            .Select(material => new GetRawMaterial(material.Id, material.Name, material.Description)).ToListAsync();
    }

    public async Task<GetRawMaterial?> GetAsync(int rawMaterialId)
    {
        var material = await context.RawMaterials.FindAsync(rawMaterialId);
        return material == null ? null : new GetRawMaterial(material.Id, material.Name, material.Description);
    }

    public async Task CreateAsync(RawMaterial entity)
    {
        await context.AddAsync(entity);
    }

    public async Task DeleteAsync(int rawMaterialId)
    {
        var material = await context.RawMaterials.FindAsync(rawMaterialId);
        context.RawMaterials.Remove(material);
    }

    public void Update(RawMaterial entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<int> SaveAsync()
    {
        return await context.SaveChangesAsync();
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