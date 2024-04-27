using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManufacturingApp.Models;

public class RecipeRawMaterial
{
    [Key]
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public int RawMaterialId { get; set; }
    public decimal Quantity { get; set; }
    
    [ForeignKey("RecipeId")]
    public virtual Recipe Recipe { get; set; }
    [ForeignKey("RawMaterialId")]
    public virtual RawMaterial RawMaterial { get; set; }
}
