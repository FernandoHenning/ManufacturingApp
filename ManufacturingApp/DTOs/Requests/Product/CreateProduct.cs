using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Requests.Product;

public record CreateProduct([Required] string Name, [Required] string Description, [Required] decimal SellingPrice);