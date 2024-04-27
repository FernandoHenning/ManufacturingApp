using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManufacturingApp.Models;

public class RecipeProduct
{
    [Key]
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    
    [ForeignKey("RecipeId")]
    public virtual Recipe Recipe { get; set; }
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
}