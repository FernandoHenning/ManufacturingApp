using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Responses.RawMaterial;

public record GetRawMaterials([Required] List<GetRawMaterial> RawMaterials);