using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Requests.RawMaterial;

public record UpdateRawMaterial([Required] string Name, [Required] string Description);