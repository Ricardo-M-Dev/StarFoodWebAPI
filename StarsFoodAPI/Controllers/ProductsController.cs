using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;
using StarsFoodAPI.Services.HttpContext;

[Authorize]
[Route("api")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly StarFoodDbContext _context;
    private readonly IProductsRepository _productsRepository;
    private readonly IVariationsRepository _variationsRepository;
    private readonly ICommandHandler<CreateProductCommand, Products> _createProductCommandHandler;
    private readonly ICommandHandler<UpdateProductCommand, Products> _updateProductCommandHandler;
    private readonly ICommandHandler<CreateVariationCommand, Variations> _createVariationCommandHandler;
    private readonly ICommandHandler<UpdateVariationCommand, Variations> _updateVariationCommandHandler;

    public ProductsController(
        StarFoodDbContext context,
        IProductsRepository productsRepository,
        IVariationsRepository variationsRepository,
        ICommandHandler<CreateProductCommand, Products> createProductCommandHandler,
        ICommandHandler<UpdateProductCommand, Products> updateProductCommandHandler,
        ICommandHandler<CreateVariationCommand, Variations> createVariationCommandHandler,
        ICommandHandler<UpdateVariationCommand, Variations> updateVariationCommandHandler
    )
    {
        _context = context;
        _productsRepository = productsRepository;
        _variationsRepository = variationsRepository;
        _createProductCommandHandler = createProductCommandHandler;
        _updateProductCommandHandler = updateProductCommandHandler;
        _createVariationCommandHandler = createVariationCommandHandler;
        _updateVariationCommandHandler = updateVariationCommandHandler;
    }

    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts([FromServices] AuthenticatedContext auth)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            List<Products>? products = await _productsRepository.GetAllAsync(restaurantId);

            if (products.Count == 0)
            {
                return NotFound();
            }
            else
            {
                foreach (var product in products)
                {
                    var category = await _context.Categories.FindAsync(product.CategoryId);
                    var variations = await _variationsRepository.GetByProductIdAsync(product.Id);

                    if (category != null)
                    {
                        product.Categories = category;
                    }

                    if (variations != null)
                    {
                        product.Variations = variations;
                    }
                }
                return Ok(products);

            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpPut("GetProduct/{id}")]
    public async Task<IActionResult> GetProductById(int id, [FromServices] AuthenticatedContext auth)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var product = await _productsRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                var category = await _context.Categories.FindAsync(product.CategoryId);
                var variations = await _variationsRepository.GetByProductIdAsync(product.Id);

                if (category != null)
                {
                    product.Categories = category;
                }

                if (variations != null)
                {
                    product.Variations = variations;
                }
                return Ok(product);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
        
    }

    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct(
        [FromServices] AuthenticatedContext auth,
        [FromBody] CreateProductCommand createProductCommand)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var newProduct = await _createProductCommandHandler.HandleAsync(createProductCommand, restaurantId);

            if (newProduct != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpPatch("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] int id,
        [FromServices] AuthenticatedContext auth,
        [FromBody] UpdateProductCommand updateProductCommand)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            updateProductCommand.Id = id;
            var updatedProduct = await _updateProductCommandHandler.HandleAsync(updateProductCommand, restaurantId);

            if (updatedProduct != null)
            {
                return Ok();
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