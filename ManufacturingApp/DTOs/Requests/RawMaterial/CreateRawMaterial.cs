using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.DTOs.Requests.RawMaterial;

public record CreateRawMaterial([Required] string Name, [Required] string Description);
