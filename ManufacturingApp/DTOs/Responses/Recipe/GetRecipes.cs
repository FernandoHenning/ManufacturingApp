using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.DTOs.Responses.Recipe;

public record GetRecipes([Required] List<GetRecipe> Recipes);