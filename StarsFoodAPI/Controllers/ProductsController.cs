using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;
using StarsFoodAPI.Services.HttpContext;

[Route("api")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly StarFoodDbContext _context;
    private readonly IProductRepository _productsRepository;
    private readonly ICommandHandler<CreateProductCommand, Products> _createProductCommandHandler;
    private readonly ICommandHandler<UpdateProductCommand, Products> _updateProductCommandHandler;
    private readonly ICommandHandler<CreateVariationCommand, Variations> _createVariationCommandHandler;

    public ProductsController(StarFoodDbContext context,
                              IProductRepository productsRepository, 
                              ICommandHandler<CreateProductCommand, Products> createProductCommandHandler, 
                              ICommandHandler<UpdateProductCommand, Products> updateProductCommandHandler, 
                              ICommandHandler<CreateVariationCommand, Variations> createVariationsCommandHandler, 
                              ICommandHandler<UpdateVariationCommand, Variations> updateVariationCommandHandler
                              )
    {
        _context = context;
        _productsRepository = productsRepository;
        _createProductCommandHandler = createProductCommandHandler;
        _updateProductCommandHandler = updateProductCommandHandler;
        _createVariationCommandHandler = createVariationsCommandHandler;
    }

    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts([FromServices] AuthenticatedContext auth)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var products = await _productsRepository.GetAllAsync(restaurantId);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpPut("GetProduct/{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productsRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct(
        [FromServices] AuthenticatedContext auth,
        [FromBody] CreateProductVariationCommand createProductModel)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var newProduct = await _createProductCommandHandler.HandleAsync(createProductModel.createProductCommand, restaurantId);

            if (newProduct != null)
            {
                foreach (var variation in createProductModel.createVariationCommandList)
                {
                    variation.ProductId = newProduct.Id;
                }

                var newVariations = await _createVariationCommandHandler.HandleAsyncList(createProductModel.createVariationCommandList, restaurantId);
                return Ok(newProduct);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpPatch("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(
        [FromServices] AuthenticatedContext auth,
        [FromBody] UpdateProductCommand updateProductCommand)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var updatedProduct = await _updateProductCommandHandler.HandleAsync(updateProductCommand, restaurantId);

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
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpDelete("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productsRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        else
        {
            await _productsRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
