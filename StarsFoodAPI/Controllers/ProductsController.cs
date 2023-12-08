using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Base;
using StarFood.Application.Communication;
using StarFood.Application.DomainModel.Commands;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Repositories;
using StarFood.Domain.ViewModels;
using StarsFoodAPI.Services.HttpContext;

[Authorize]
[Route("api")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IVariationsRepository _variationsRepository;
    private readonly IRestaurantsRepository _restaurantRepository;

    public ProductsController(
        ICategoriesRepository categoriesRepository,
        IVariationsRepository variationsRepository,
        IRestaurantsRepository restaurantsRepository
    )
    {
        _categoriesRepository = categoriesRepository;
        _variationsRepository = variationsRepository;
        _restaurantRepository = restaurantsRepository;
    }

    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<ProductsViewModel>> GetAllProducts(
        [FromServices] IProductsRepository repository,
        [FromServices] IMapper map,
        [FromServices] RequestState requestContext
    )
    {
        try
        {
            var restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
            if (restaurant == null)
            {
                return BadRequest(new DomainException($"Restaurante de ID {requestContext.RestaurantId} não pode ser encontrado."));
            }

            var list = repository.GetProductsByRestaurantId(restaurant);

            foreach (var product in list)
            {
                var category = _categoriesRepository.GetCategoryById(restaurant, product.CategoryId);
                var variations = _variationsRepository.GetVariationsByProductId(restaurant, product.Id);

                if (category != null)
                {
                    product.Category = category;
                }

                if (variations != null)
                {
                    product.Variations = variations;
                }
            }

            var result = map.Map<List<ProductsViewModel>>(list.AsQueryable());

            return Ok(result.ToArray().OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("GetProduct/{id}")]
    public async Task<ActionResult<ProductsViewModel>> GetProductById(
        [FromRoute] int id,
        [FromServices] IProductsRepository repository,
        [FromServices] IMapper map,
        [FromServices] RequestState requestContext
    )
    {
        try
        {
            var restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
            if (restaurant == null)
            {
                return BadRequest(new DomainException($"Restaurante de ID {requestContext.RestaurantId} não pode ser encontrado."));
            }

            var product = repository.GetProductById(restaurant, id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                var category = _categoriesRepository.GetCategoryById(restaurant, product.CategoryId);
                var variations = _variationsRepository.GetVariationsByProductId(restaurant, product.Id);

                if (category != null)
                {
                    product.Category = category;
                }

                if (variations != null)
                {
                    product.Variations = variations;
                }

                var result = map.Map<ProductsViewModel>(product);

                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }

    }

    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductCommand cmd,
        [FromServices] IMediatorHandler mediator,
        [FromServices] IMapper map,
        [FromServices] IHostApplicationLifetime appLifetime,
        [FromServices] RequestState requestContext
    )
    {
        var restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
        if (restaurant == null)
        {
            return BadRequest(new DomainException($"Restaurant de ID {requestContext.RestaurantId} não pode ser encontrado."));
        }

        cmd.UpdateRequestInfo(requestContext, restaurant);

        var result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

        if (result.IsValid)
        {
            result.Object = map.Map<ProductsViewModel>(result.Object);
            return Ok();
        }

        return BadRequest(result);
    }

    [HttpPatch("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct(
        [FromBody] UpdateProductCommand cmd,
        [FromServices] IMediatorHandler mediator,
        [FromServices] IMapper map,
        [FromServices] IHostApplicationLifetime appLifetime,
        [FromServices] RequestState requestContext
    )
    {
        var restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
        if (restaurant == null)
        {
            return BadRequest(new DomainException($"Restaurant de ID {requestContext.RestaurantId} não pode ser encontrado."));
        }

        cmd.UpdateRequestInfo(requestContext, restaurant);

        var result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

        if (result.IsValid)
        {
            result.Object = map.Map<ProductsViewModel>(result.Object);
            return Ok();
        }

        return BadRequest(result);
    }

    [HttpDelete("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(
        [FromRoute] int id,
        [FromServices] IMediatorHandler mediator,
        [FromServices] IMapper map,
        [FromServices] IHostApplicationLifetime appLifetime,
        [FromServices] RequestState requestContext
    )
    {
        var restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
        if (restaurant == null)
        {
            return BadRequest(new DomainException($"Restaurant de ID {requestContext.RestaurantId} não pode ser encontrado."));
        }

        var cmd = new DeleteProductCommand()
        {
            Id = id,
            Restaurant = restaurant
        };

        var result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

        if (result.IsValid)
        {
            result.Object = map.Map<ProductsViewModel>(result.Object);
            return Ok(result);
        }

        return BadRequest(result);
    }
}