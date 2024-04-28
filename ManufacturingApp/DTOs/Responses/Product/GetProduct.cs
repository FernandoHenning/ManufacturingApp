using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Responses.Product;

public record GetProduct(
    [Required] int Id,
    [Required] string Name,
    [Required] string Description,
    [Required] decimal SellingPrice);