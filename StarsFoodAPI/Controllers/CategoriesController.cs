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
public class CategoriesController : ControllerBase
{
    private readonly IRestaurantsRepository _restaurantRepository;

    public CategoriesController(
        IRestaurantsRepository restaurantsRepository
    )
    {
        _restaurantRepository = restaurantsRepository;
    }

    [HttpGet("GetAllCategories")]
    public async Task<ActionResult<CategoriesViewModel>> GetAllCategories(
        [FromServices] ICategoriesRepository repository,
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

            var list = repository.GetCategoriesByRestaurantId(restaurant);

            var result = map.Map<List<CategoriesViewModel>>(list.AsQueryable());

            return Ok(result.ToArray().OrderBy(c => c.CategoryName, StringComparer.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("GetCategory/{id}")]
    public async Task<ActionResult<CategoriesViewModel>> GetCategoryById(
        [FromRoute] int id,
        [FromServices] ICategoriesRepository repository,
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
            var category = repository.GetCategoryById(restaurant, id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                var result = map.Map<CategoriesViewModel>(category);

                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost("CreateCategory")]
    public async Task<IActionResult> CreateCategory(
        [FromBody] CreateCategoryCommand cmd,
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
            result.Object = map.Map<CategoriesViewModel>(result.Object);
            return Ok();
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPatch("UpdateCategory")]
    public async Task<IActionResult> UpdateCategory(
        [FromBody] UpdateCategoryCommand cmd,
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
            result.Object = map.Map<CategoriesViewModel>(result.Object);
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpDelete("DeleteCategory/{id}")]
    public async Task<IActionResult> DeleteCategory(
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

        var cmd = new DeleteCategoryCommand()
        {
            Id = id,
            Restaurant = restaurant
        };

        var result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

        if (result.IsValid)
        {
            result.Object = map.Map<CategoriesViewModel>(result.Object);
            return Ok(result);
        }

        return BadRequest();
    }
}
