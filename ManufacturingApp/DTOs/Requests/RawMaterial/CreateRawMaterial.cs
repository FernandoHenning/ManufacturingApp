using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Requests.RawMaterial;

public record CreateRawMaterial([Required] string Name, [Required] string Description);
