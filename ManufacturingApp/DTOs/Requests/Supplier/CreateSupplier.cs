using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Requests.Supplier;

public record CreateSupplier([Required] string Name, string Description);