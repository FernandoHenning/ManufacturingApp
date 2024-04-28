using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.Requests.Recipe;

public record CreateRecipe(
    [Required] string Name,
    string Description,
    List<int> RawMaterialsIds,
    List<int> ProductsIds);