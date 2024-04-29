using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.DTOs.Responses.RawMaterial;

public record GetRawMaterial([Required] int Id, [Required] string Name, [Required] string Description);