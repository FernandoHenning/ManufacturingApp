using ManufacturingApp.DataAccess.Repositories;
using ManufacturingApp.DTOs.Requests.RawMaterial;
using ManufacturingApp.DTOs.Responses.RawMaterial;
using ManufacturingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManufacturingApp.Controllers;

[ApiController]
[Route("[controller]")]
public class RawMaterialsController(RawMaterialRepository repository, ILogger<RawMaterialsController> logger) : ControllerBase
{
    /// <summary>
    /// Get raw material by ID.
    /// </summary>
    /// <param name="id">The ID of the raw material to retrieve.</param>
    /// <returns>The raw material information.</returns>
    [HttpGet("{id:int}")]
    
    [ProducesResponseType(typeof(GetRawMaterial), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult<GetRawMaterial>> GetAsync(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid ID. ID must be a positive integer.");
        }

        try
        {
            var rawMaterial = await repository.GetAsync(id);

            if (rawMaterial == null)
            {
                return NotFound("Raw material not found.");
            }

            return Ok(new GetRawMaterial
            (
                Id: rawMaterial.Id,
                Name: rawMaterial.Name,
                Description: rawMaterial.Description
            ));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching raw material");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Get a list of all raw materials
    /// </summary>
    /// <returns>A list of raw materials' information.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetRawMaterial>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult<IEnumerable<GetRawMaterial>>> GetAllAsync()
    {
        try
        {
            var rawMaterials = await repository.GetAllAsync();

            if (!rawMaterials.Any())
            {
                return NotFound("No raw materials found.");
            }

            return Ok(rawMaterials);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching raw materials");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
    
    /// <summary>
    /// Create a new raw material.
    /// </summary>
    /// <param name="request">The raw material creation request.</param>
    /// <returns>The created raw material's ID.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult> Post([FromBody] CreateRawMaterial request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var rawMaterial = new RawMaterial { Name = request.Name, Description = request.Description };

        try
        {
            await repository.CreateAsync(rawMaterial);
            await repository.SaveAsync();


            return Created(nameof(GetAsync), new { id = rawMaterial.Id });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating raw material");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
    
    /// <summary>
    /// Update raw material by ID.
    /// </summary>
    /// <param name="id">The ID of the raw material to update.</param>
    /// <param name="request">The raw material update request.</param>
    /// <returns>No content.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType( 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateRawMaterial request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await repository.ExistsByIdAsync(id))
        {
            return NotFound("Raw material not found.");
        }

        try
        {
            repository.Update(new RawMaterial { Name = request.Name, Description = request.Description, Id = id });
            await repository.SaveAsync();

            return Ok();
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Error creating raw material");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Delete raw material by ID.
    /// </summary>
    /// <param name="id">The ID of the raw material to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType( 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult> Delete(int id)
    {
        if (!await repository.ExistsByIdAsync(id))
        {
            return NotFound("Raw material not found.");
        }

        try
        {
            await repository.DeleteAsync(id);
            await repository.SaveAsync();
            return Ok();
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Error creating raw material");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
