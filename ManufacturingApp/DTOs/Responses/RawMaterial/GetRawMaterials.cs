using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.DTOs.Responses.RawMaterial;

public record GetRawMaterials([Required] List<GetRawMaterial> RawMaterials);