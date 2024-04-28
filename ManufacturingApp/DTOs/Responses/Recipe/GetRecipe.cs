using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Responses.Recipe;

public record GetRecipe([Required] int Id, [Required] string Name, string Description);