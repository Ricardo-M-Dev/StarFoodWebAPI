using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Base;
using StarFood.Application.Base.Messages;
using StarFood.Application.Communication;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Domain.ViewModels;
using StarsFoodAPI.Services.HttpContext;

[Authorize]
[Route("api/restaurants/")]
[ApiController]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantsRepository _restaurantRepository;

    public RestaurantsController(IRestaurantsRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantById(
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
            else
            {
                RestaurantsViewModel? restaurantViewMode = map.Map<RestaurantsViewModel>(restaurant);
                return Ok(restaurantViewMode);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(
        [FromBody] CreateRestaurantCommand cmd,
        [FromServices] IMediatorHandler mediator,
        [FromServices] IHostApplicationLifetime appLifetime,
        [FromServices] RequestState requestContext
    )
    {
        try
        {
            Restaurants? restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);

            if (restaurant != null)
            {
                int restaurantId = restaurant.RestaurantId;
                return NotFound(new DomainException($"Restaurante de ID {restaurantId} já se encontra cadastrado."));
            }

            cmd.RestaurantId = requestContext.NRestaurantId;

            ICommandResponse? result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

            if (result.IsValid)
            {
                return Created($"/api/products/{result.Id}", result.Object);
            }

            return BadRequest(result.Exception.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRestaurant(
        [FromRoute] int id,
        [FromBody] UpdateRestaurantCommand cmd,
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
                return NotFound(new DomainException($"Restaurant de ID {restaurantId} não pode ser encontrado."));
            }

            cmd.UpdateRequestInfo(requestContext, restaurant);
            cmd.Id = id;
            cmd.RestaurantId = restaurantId;

            ICommandResponse? result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

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
        [FromBody] StatusRestaurantCommand cmd,
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

            ICommandResponse? result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

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
