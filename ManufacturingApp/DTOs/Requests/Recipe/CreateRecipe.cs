using System.ComponentModel.DataAnnotations;

namespace ManufacturingApp.DTOs.Requests.Recipe;

public record CreateRecipe(
    [Required] string Name,
    string Description,
    List<RawMaterialRecipeInfo> RawMaterials,
    List<ProductRecipeInfo> Products);

public record RawMaterialRecipeInfo([Required] int RawMaterialId, [Required] decimal Quantity);

public record ProductRecipeInfo([Required] int ProductId, [Required] decimal Quantity);