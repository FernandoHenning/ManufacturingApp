using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.DTOs.Requests.Supplier;

public record CreateSupplier([Required] string Name, string Description);