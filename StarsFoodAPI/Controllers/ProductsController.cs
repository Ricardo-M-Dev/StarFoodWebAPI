using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Application.Models;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data.Repositories;
using StarsFoodAPI.Services.HttpContext;
using System.Runtime.InteropServices;

[Route("api")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productsRepository;
    private readonly IVariationsRepository _variationsRepository;
    private readonly IProductVariationsRepository _productVariationsRepository;
    private readonly ICommandHandler<CreateProductCommand, Products> _createProductCommandHandler;
    private readonly ICommandHandler<UpdateProductCommand, Products> _updateProductCommandHandler;
    private readonly ICommandHandler<CreateVariationCommand, Variations> _createVariationCommandHandler;
    private readonly ICommandHandler<UpdateVariationCommand, Variations> _updateVariationCommandHandler;
    private readonly ICommandHandler<CreateProductVariationCommand, ProductVariations> _createProductVariationCommandHandler;

    public ProductsController(IProductRepository productsRepository, 
                              IVariationsRepository variationsRepository,
                              IProductVariationsRepository productVariationsRepository,
                              ICommandHandler<CreateProductCommand, Products> createProductCommandHandler, 
                              ICommandHandler<UpdateProductCommand, Products> updateProductCommandHandler, 
                              ICommandHandler<CreateVariationCommand, Variations> createVariationsCommandHandler, 
                              ICommandHandler<UpdateVariationCommand, Variations> updateVariationCommandHandler,
                              ICommandHandler<CreateProductVariationCommand, ProductVariations> createProductVariationCommandHandler)
    {
        _productsRepository = productsRepository;
        _variationsRepository = variationsRepository;
        _productVariationsRepository = productVariationsRepository;
        _createProductCommandHandler = createProductCommandHandler;
        _updateProductCommandHandler = updateProductCommandHandler;
        _createVariationCommandHandler = createVariationsCommandHandler;
        _updateVariationCommandHandler = updateVariationCommandHandler;
        _createProductVariationCommandHandler = createProductVariationCommandHandler;
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

    [HttpGet("GetProduct/{id}")]
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
        [FromBody] CreateProductModel createProductModel)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var newProduct = await _createProductCommandHandler.HandleAsync(createProductModel.ProductCommand, restaurantId);

            if (newProduct != null)
            {
                List<CreateProductVariationCommand> productVariationList = new List<CreateProductVariationCommand>();

                var variations = await _createVariationCommandHandler.HandleAsyncList(createProductModel.VariationCommand, restaurantId);
                if (variations != null)
                {
                    foreach (var variation in variations)
                    {
                        var productVariation = new CreateProductVariationCommand();
                        productVariation.ProductId = newProduct.Id;
                        productVariation.VariationId = variation.Id;
                        productVariationList.Add(productVariation);
                    }

                    if (productVariationList != null)
                    {
                        await _createProductVariationCommandHandler.HandleAsyncList(productVariationList, restaurantId);
                    }
                }
            }
            else
            {
                return BadRequest();
            }

            return Ok(newProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status406NotAcceptable, ex.Message);
        }
    }

    [HttpPatch("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(
        [FromServices] AuthenticatedContext auth,
        [FromBody] UpdateProductModel updateProductModel)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var updatedProduct = await _updateProductCommandHandler.HandleAsync(updateProductModel.ProductCommand, restaurantId);

            if (updatedProduct != null)
            {
                var productVariations = await _productVariationsRepository.GetByProductId(updateProductModel.ProductCommand.Id);

                if (productVariations != null)
                {
                    foreach (var item in productVariations)
                    {
                        var variation = await _variationsRepository.GetByIdAsync(item.Id);
                        await _variationsRepository.UpdateAsync(item.Id, variation);
                    }
                }
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

        var existingProduct = await _productsRepository.GetByIdAsync(id);
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
        var product = await _productsRepository.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        await _productsRepository.DeleteAsync(id);
        return Ok();
    }
}
