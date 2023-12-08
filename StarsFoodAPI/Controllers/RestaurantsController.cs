using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Base;
using StarFood.Application.Communication;
using StarFood.Domain.Commands;
using StarFood.Domain.Repositories;
using StarFood.Domain.ViewModels;
using StarsFoodAPI.Services.HttpContext;

[Authorize]
[Route("api")]
[ApiController]
public class RestaurantsController : ControllerBase
{

    public RestaurantsController()
    {

    }

    [HttpGet("GetRestaurant/{id}")]
    public async Task<IActionResult> GetRestaurantById(
        [FromServices] IRestaurantsRepository repository,
        [FromServices] IMapper map,
        [FromServices] RequestState requestContext
    )
    {
        var restaurant = repository.GetRestaurantById(requestContext.RestaurantId);
        if (restaurant == null)
        {
            return BadRequest(new DomainException($"Restaurante de ID {requestContext.RestaurantId} não pode ser encontrado."));
        }
        else
        {
            var result = map.Map<RestaurantsViewModel>(restaurant);
            return Ok(result);
        }
    }

    [HttpPost("CreateRestaurant")]
    public async Task<IActionResult> CreateRestaurant(
        [FromBody] CreateRestaurantCommand cmd,
        [FromServices] IRestaurantsRepository repository,
        [FromServices] IMediatorHandler mediator,
        [FromServices] IMapper map,
        [FromServices] IHostApplicationLifetime appLifetime,
        [FromServices] RequestState requestContext
    )
    {
        var restaurant = repository.GetRestaurantById(requestContext.RestaurantId);

        if (restaurant != null)
        {
            cmd.UpdateRequestInfo(requestContext, restaurant);
        }

        var result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

        if (result.IsValid)
        {
            result.Object = map.Map<RestaurantsViewModel>(result.Object);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPut("UpdateRestaurant/{id}")]
    public async Task<IActionResult> UpdateRestaurant(
        [FromBody] UpdateRestaurantCommand cmd,
        [FromServices] IRestaurantsRepository repository,
        [FromServices] IMediatorHandler mediator,
        [FromServices] IMapper map,
        [FromServices] IHostApplicationLifetime appLifetime,
        [FromServices] RequestState requestContext
    )
    {
        var restaurant = repository.GetRestaurantById(requestContext.RestaurantId);
        if (restaurant == null)
        {
            return BadRequest(new DomainException($"Restaurant de ID {requestContext.RestaurantId} não pode ser encontrado."));
        }

        cmd.UpdateRequestInfo(requestContext, restaurant);

        var result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

        if (result.IsValid)
        {
            result.Object = map.Map<RestaurantsViewModel>(result.Object);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}
