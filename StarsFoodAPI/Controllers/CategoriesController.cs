using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Base;
using StarFood.Application.Base.Messages;
using StarFood.Application.Communication;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Domain.ViewModels.Products;
using StarsFoodAPI.Services.HttpContext;

[Authorize]
[Route("api/categories/")]
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

    [HttpGet]
    public async Task<ActionResult<CategoriesViewModel>> GetAllCategories(
        [FromServices] ICategoriesRepository repository,
        [FromServices] IMapper map,
        [FromServices] RequestState requestContext
    )
    {
        try
        {
            Restaurants? restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
            int restaurantId = restaurant.RestaurantId;

            if (restaurant == null)
            {
                return NotFound(new DomainException($"Restaurante de ID {restaurantId} não pode ser encontrado."));
            }

            List<Categories>? categories = repository.GetCategoriesByRestaurantId(restaurantId);

            if (categories == null)
            {
                return NotFound(new DomainException($"Nenhuma Categories do Restaurante de ID {restaurantId} foi encontrado."));
            }

            List<CategoriesViewModel>?result = map.Map<List<CategoriesViewModel>>(categories.AsQueryable());

            return Ok(result.ToArray().OrderBy(c => c.Name, StringComparer.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriesViewModel>> GetCategoryById(
        [FromRoute] int id,
        [FromServices] ICategoriesRepository repository,
        [FromServices] IMapper map,
        [FromServices] RequestState requestContext
    )
    {
        try
        {
            Restaurants? restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
            int restaurantId = restaurant.RestaurantId;

            if (restaurant == null)
            {
                return NotFound(new DomainException($"Restaurante de ID {restaurantId} não pode ser encontrado."));
            }
            Categories? category = repository.GetCategoryById(id, restaurantId);

            if (category == null)
            {
                return NotFound(new DomainException($"Nenhuma Categoria de ID {id} foi encontrada."));
            }
            else
            {
                CategoriesViewModel? result = map.Map<CategoriesViewModel>(category);

                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(
        [FromBody] CreateCategoryCommand cmd,
        [FromServices] IMediatorHandler mediator,
        [FromServices] IHostApplicationLifetime appLifetime,
        [FromServices] RequestState requestContext
    )
    {
        try
        {
            Restaurants? restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
            int restaurantId = restaurant.RestaurantId;

            if (restaurant == null)
            {
                return BadRequest(new DomainException($"Restaurant de ID {restaurantId} não pode ser encontrado."));
            }

            cmd.UpdateRequestInfo(requestContext, restaurant);

            ICommandResponse result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

            if (result.IsValid)
            {
                return Created($"/api/categories/{result.Id}", result.Object);
            }
            else
            {
                return BadRequest(result.Exception.Message);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(
        [FromRoute] int id,
        [FromBody] UpdateCategoryCommand cmd,
        [FromServices] IMediatorHandler mediator,
        [FromServices] IHostApplicationLifetime appLifetime,
        [FromServices] RequestState requestContext
        )
    {
        try
        {
            Restaurants? restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
            int restaurantId = restaurant.RestaurantId;

            if (restaurant == null)
            {
                return BadRequest(new DomainException($"Restaurant de ID {restaurantId} não pode ser encontrado."));
            }

            cmd.UpdateRequestInfo(requestContext, restaurant);
            cmd.Id = id;
            cmd.RestaurantId = restaurantId;

            ICommandResponse result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

            if (result.IsValid)
            {
                return NoContent();
            }

            return BadRequest(result.Exception.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> SetStatus(
        [FromRoute] int id,
        [FromBody] StatusCategoryCommand cmd,
        [FromServices] IMediatorHandler mediator,
        [FromServices] IHostApplicationLifetime appLifetime,
        [FromServices] RequestState requestContext
        )
    {
        try
        {
            Restaurants? restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
            int restaurantId = restaurant.RestaurantId;
            
            if (restaurant == null)
            {
                return BadRequest(new DomainException($"Restaurant de ID {restaurantId} não pode ser encontrado."));
            }

            cmd.UpdateRequestInfo(requestContext, restaurant);
            cmd.Id = id;
            cmd.RestaurantId = restaurantId;

            ICommandResponse result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

            if (result.IsValid)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception)
        {

            throw;
        }
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(
        [FromBody] DeleteCategoryCommand cmd,
        [FromServices] IMediatorHandler mediator,
        [FromServices] IHostApplicationLifetime appLifetime,
        [FromServices] RequestState requestContext
    )
    {
        try
        {
            Restaurants? restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
            if (restaurant == null)
            {
                return BadRequest(new DomainException($"Restaurant de ID {requestContext.RestaurantId} não pode ser encontrado."));
            }

            ICommandResponse result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

            if (result.IsValid)
            {
                return NoContent();
            }

            return BadRequest(result.Exception.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
