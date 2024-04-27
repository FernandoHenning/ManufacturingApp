using ManufacturingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ManufacturingApp.DataAccess;

public class ManufacturingDbContext(DbContextOptions<ManufacturingDbContext> options) : DbContext(options)
{
    public DbSet<RawMaterial> RawMaterials { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierRawMaterial> SupplierRawMaterials { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeRawMaterial> RecipeRawMaterials { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<RecipeProduct> RecipeProducts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupplierRawMaterial>()
            .HasKey(sr => new { sr.SupplierId, sr.RawMaterialId });

        modelBuilder.Entity<RecipeRawMaterial>()
            .HasKey(rr => new { rr.RecipeId, rr.RawMaterialId });

        modelBuilder.Entity<RecipeProduct>()
            .HasKey(rp => new { rp.RecipeId, rp.ProductId });
    }
}