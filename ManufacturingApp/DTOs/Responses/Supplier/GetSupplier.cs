using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Responses.Supplier;

public record GetSupplier([Required] int SupplierId, [Required] string Name, [Required] string Description);