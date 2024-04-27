using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManufacturingApp.Models;

public class SupplierRawMaterial
{
    [Key] public int Id { get; set; }
    public int SupplierId { get; set; }
    public int RawMaterialId { get; set; }
    public decimal Price { get; set; }
    [ForeignKey("SupplierId")] public virtual Supplier Supplier { get; set; }
    [ForeignKey("RawMaterialId")] public virtual RawMaterial RawMaterial { get; set; }
}