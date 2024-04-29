using ManufacturingApp.DataAccess.Repositories;
using ManufacturingApp.DTOs.Requests.Recipe;
using ManufacturingApp.DTOs.Responses.Recipe;
using ManufacturingApp.DTOs.Responses.Supplier;
using ManufacturingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManufacturingApp.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipesController(RecipeRepository repository, ILogger<RecipesController> logger) : ControllerBase
{
    /// <summary>
    /// Get recipe by ID.
    /// </summary>
    /// <param name="id">The ID of the recipe to retrieve.</param>
    /// <returns>The recipe information.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GetRecipe), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult<GetRecipe>> GetAsync(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid ID. ID must be a positive integer.");
        }

        try
        {
            var recipe = await repository.GetAsync(id);

            if (recipe == null)
            {
                return NotFound("Recipe not found.");
            }

            return Ok(new GetRecipe
            (
                Id: recipe.SupplierId,
                Name: recipe.Name,
                Description: recipe.Description
            ));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching recipe");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Get a list of all recipes
    /// </summary>
    /// <returns>A list of recipes' information.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetRecipe>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult<IEnumerable<GetRecipe>>> GetAllAsync()
    {
        try
        {
            var recipes = await repository.GetAllAsync();

            if (!recipes.Any())
            {
                return NotFound("No recipes found.");
            }

            return Ok(recipes);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching recipes");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Create a new recipe.
    /// </summary>
    /// <param name="request">The recipe creation request.</param>
    /// <returns>The created recipe's ID.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult> Post([FromBody] CreateRecipe request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var recipe = new Recipe { Name = request.Name, Description = request.Description };

        try
        {
            await repository.CreateAsync(recipe);
            await repository.SaveAsync();
            foreach (var material in request.RawMaterials)
                await repository.AddRawMaterialAsync(material, recipe.Id);

            foreach (var product in request.Products)
                await repository.AddProductAsync(product, recipe.Id);

            await repository.SaveAsync();

            return Created(nameof(GetAsync), new { id = recipe.Id });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating recipe");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Update recipe by ID.
    /// </summary>
    /// <param name="id">The ID of the recipe to update.</param>
    /// <param name="request">The recipe update request.</param>
    /// <returns>No content.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateRecipeInfo request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await repository.ExistsByIdAsync(id))
        {
            return NotFound("Recipe not found.");
        }

        try
        {
            repository.Update(new Recipe { Name = request.Name, Description = request.Description, Id = id });
            await repository.SaveAsync();

            return Ok();
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Error creating recipe");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Delete recipe by ID.
    /// </summary>
    /// <param name="id">The ID of the recipe to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult> Delete(int id)
    {
        if (!await repository.ExistsByIdAsync(id)) return NotFound("Recipe not found.");

        try
        {
            await repository.DeleteAsync(id);
            await repository.SaveAsync();
            return Ok();
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Error creating recipe");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpGet("OptimizeSuppliers/{recipeId:int}")]
    public async Task<ActionResult<OptimizedSuppliers>> OptimizeSuppliers(int recipeId)
    {
        if (!await repository.ExistsByIdAsync(recipeId)) return NotFound("Recipe not found.");
        
        var prices = repository.GetOptimalSuppliers(recipeId);
        if (prices.Count == 0) return NotFound();
        
        return Ok(new OptimizedSuppliers(prices));
    }
}