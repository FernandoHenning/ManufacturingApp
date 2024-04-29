using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.DTOs.Responses.Supplier;

public record GetSuppliers([Required] List<GetSupplier> Suppliers);