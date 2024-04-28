using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Requests.Product;

public record UpdateProductInfo(
    [Required] int Id,
    [Required] string Name,
    [Required] string Description,
    [Required] decimal SellingPrice);