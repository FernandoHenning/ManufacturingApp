using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.DTOs.Responses.Supplier;

public record OptimizedSuppliers([Required] List<SupplierRawMaterialPrice> Prices);

public record SupplierRawMaterialPrice(
    [Required] string SupplierName,
    [Required] string RawMaterialName,
    [Required] decimal SupplierPrice
);