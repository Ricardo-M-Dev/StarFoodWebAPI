using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using System.Runtime.InteropServices;

[Route("api")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productesRepository;
    private readonly ICommandHandler<CreateProductCommand, Products> _createProductCommandHandler;
    private readonly ICommandHandler<UpdateProductCommand, Products> _updateProductCommandHandler;

    public ProductsController(IProductRepository productesRepository, ICommandHandler<CreateProductCommand, Products> createProductCommandHandler, ICommandHandler<UpdateProductCommand, Products> updateProductCommandHandler)
    {
        _productesRepository = productesRepository;
        _createProductCommandHandler = createProductCommandHandler;
        _updateProductCommandHandler = updateProductCommandHandler;
    }

    [HttpGet("GetAllProductes")]
    public async Task<IActionResult> GetAllProductes(int restaurantId)
    {
        var productes = await _productesRepository.GetAllAsync(restaurantId);
        return Ok(productes);
    }

    [HttpGet("GetProduct/{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productesRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand createProductCommand)
    {
        try
        {
            var newProduct = await _createProductCommandHandler.HandleAsync(createProductCommand);

            if (newProduct != null)
            {
                return Ok(newProduct);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status406NotAcceptable, ex.Message);
        }
    }

    [HttpPatch("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand updateProductCommand)
    {
        try
        {
            var updatedProduct = await _updateProductCommandHandler.HandleAsync(updateProductCommand);

            if (updatedProduct != null)
            {
                return Ok(updatedProduct);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status406NotAcceptable, ex.Message);
        }
    }

    [HttpPatch("SetProductAvailability/{id}")]
    public async Task<IActionResult> SetAvailability(int id, bool isAvailable)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingProduct = await _productesRepository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        existingProduct.SetAvailability(isAvailable);
        return Ok(existingProduct);
    }

    [HttpDelete("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productesRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        await _productesRepository.DeleteAsync(id);
        return Ok();
    }
}
