using ManufacturingApp.DataAccess.Repositories;
using ManufacturingApp.DTOs.Requests.Product;
using ManufacturingApp.DTOs.Responses.Product;
using ManufacturingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManufacturingApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(ProductRepository repository, ILogger<ProductsController> logger) : ControllerBase
{
    /// <summary>
    /// Get product by ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>The product information.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GetProduct), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult<GetProduct>> GetAsync(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid ID. ID must be a positive integer.");
        }

        try
        {
            var product = await repository.GetAsync(id);

            if (product == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(new GetProduct
            (
                Id: product.Id,
                Name: product.Name,
                Description: product.Description,
                SellingPrice:product.SellingPrice
            ));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching product");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Get a list of all products
    /// </summary>
    /// <returns>A list of products' information.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetProduct>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult<IEnumerable<GetProduct>>> GetAllAsync()
    {
        try
        {
            var products = await repository.GetAllAsync();

            if (!products.Any())
            {
                return NotFound("No products found.");
            }

            return Ok(products);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching products");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Create a new product.
    /// </summary>
    /// <param name="request">The product creation request.</param>
    /// <returns>The created product's ID.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult> Post([FromBody] CreateProduct request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = new Product { Name = request.Name, Description = request.Description, SellingPrice = request.SellingPrice};

        try
        {
            await repository.CreateAsync(product);
            await repository.SaveAsync();


            return Created(nameof(GetAsync), new { id = product.Id });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating product");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Update product by ID.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="request">The product update request.</param>
    /// <returns>No content.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateProductInfo request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await repository.ExistsByIdAsync(id))
        {
            return NotFound("Product not found.");
        }

        try
        {
            repository.Update(new Product { Name = request.Name, Description = request.Description, Id = id, SellingPrice = request.SellingPrice});
            await repository.SaveAsync();

            return Ok();
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Error creating product");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    /// <summary>
    /// Delete product by ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<ActionResult> Delete(int id)
    {
        if (!await repository.ExistsByIdAsync(id))
        {
            return NotFound("Product not found.");
        }

        try
        {
            await repository.DeleteAsync(id);
            await repository.SaveAsync();
            return Ok();
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Error creating product");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}