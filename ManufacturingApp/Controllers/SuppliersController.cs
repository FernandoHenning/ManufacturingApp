using ManufacturingApp.DataAccess.Repositories;
using ManufacturingApp.DTOs.Requests.Supplier;
using ManufacturingApp.DTOs.Responses.Supplier;
using ManufacturingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManufacturingApp.Controllers;

[ApiController]
[Route("[controller]")]
public class SuppliersController(SupplierRepository repository, ILogger<SuppliersController> logger) : ControllerBase
{
    /// <summary>
    /// Get supplier by ID.
    /// </summary>
    /// <param name="id">The ID of the supplier to retrieve.</param>
    /// <returns>The supplier information.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GetSupplier), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult<GetSupplier>> GetAsync(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid ID. ID must be a positive integer.");
        }

        try
        {
            var supplier = await repository.GetAsync(id);

            if (supplier == null)
            {
                return NotFound("Supplier not found.");
            }

            return Ok(new GetSupplier
            (
                SupplierId: supplier.SupplierId,
                Name: supplier.Name,
                Description: supplier.Description
            ));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching supplier");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Get a list of all suppliers
    /// </summary>
    /// <returns>A list of suppliers' information.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetSupplier>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult<IEnumerable<GetSupplier>>> GetAllAsync()
    {
        try
        {
            var suppliers = await repository.GetAllAsync();

            if (!suppliers.Any())
            {
                return NotFound("No suppliers found.");
            }

            return Ok(suppliers);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching suppliers");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Create a new supplier.
    /// </summary>
    /// <param name="request">The supplier creation request.</param>
    /// <returns>The created supplier's ID.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult> Post([FromBody] CreateSupplier request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var supplier = new Supplier { Name = request.Name, Description = request.Description };

        try
        {
            await repository.CreateAsync(supplier);
            await repository.SaveAsync();


            return Created(nameof(GetAsync), new { id = supplier.Id });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating supplier");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Adds a raw material to a supplier
    /// </summary>
    /// <param name="request">The add raw material to supplier request.</param>
    /// <returns>No content.</returns>
    [HttpPost("/AddRawMaterial")]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult> AddRawMaterial([FromBody] AddRawMaterialToSupplier request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await repository.AddRawMaterialToSupplierAsync(request.RawMaterialId, request.SupplierId, request.Price);
            await repository.SaveAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding the raw material to the supplier");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }


    /// <summary>
    /// Update supplier by ID.
    /// </summary>
    /// <param name="id">The ID of the supplier to update.</param>
    /// <param name="request">The supplier update request.</param>
    /// <returns>No content.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateSupplier request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await repository.ExistsByIdAsync(id))
        {
            return NotFound("Supplier not found.");
        }

        try
        {
            repository.Update(new Supplier { Name = request.Name, Description = request.Description, Id = id });
            await repository.SaveAsync();

            return Ok();
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Error creating supplier");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Delete supplier by ID.
    /// </summary>
    /// <param name="id">The ID of the supplier to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult> Delete(int id)
    {
        if (!await repository.ExistsByIdAsync(id))
        {
            return NotFound("Supplier not found.");
        }

        try
        {
            await repository.DeleteAsync(id);
            await repository.SaveAsync();
            return Ok();
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Error creating supplier");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}