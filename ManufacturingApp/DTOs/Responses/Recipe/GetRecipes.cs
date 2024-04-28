using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Responses.Recipe;

public record GetRecipes([Required] List<GetRecipe> Recipes);