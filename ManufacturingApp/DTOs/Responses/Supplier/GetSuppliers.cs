using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Responses.Supplier;

public record GetSuppliers([Required] List<GetSupplier> Suppliers);