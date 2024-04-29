using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.DTOs.Requests.Supplier;

public record AddRawMaterialToSupplier(
    [Required] int RawMaterialId,
    [Required] int SupplierId,
    [Required] decimal Price);