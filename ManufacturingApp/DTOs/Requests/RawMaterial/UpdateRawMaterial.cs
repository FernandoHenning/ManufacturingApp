using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.DTOs.Requests.RawMaterial;

public record UpdateRawMaterial([Required] string Name, [Required] string Description);