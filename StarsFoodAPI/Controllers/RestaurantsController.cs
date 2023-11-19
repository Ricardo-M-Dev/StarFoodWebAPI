using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarsFoodAPI.Services.HttpContext;

[Authorize]
[Route("api")]
[ApiController]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantsRepository _restaurantsRepository;
    private readonly ICommandHandler<CreateRestaurantCommand, Restaurants> _createRestaurantCommandHandler;
    private readonly ICommandHandler<UpdateRestaurantCommand, Restaurants> _updateRestaurantCommandHandler;

    public RestaurantsController(IRestaurantsRepository restaurantsRepository,
                                 ICommandHandler<CreateRestaurantCommand, Restaurants> createRestaurantCommandHandler,
                                 ICommandHandler<UpdateRestaurantCommand, Restaurants> updateRestaurantCommandHandler
                                 )
    {
        _restaurantsRepository = restaurantsRepository;
        _createRestaurantCommandHandler = createRestaurantCommandHandler;
        _updateRestaurantCommandHandler = updateRestaurantCommandHandler;
    }

    [HttpGet("GetAllRestaurants")]
    public async Task<IActionResult> GetAllRestaurants()
    {
        var restaurants = await _restaurantsRepository.GetAllAsync();
        return Ok(restaurants);
    }

    [HttpGet("GetRestaurant/{id}")]
    public async Task<IActionResult> GetRestaurantById([FromServices] AuthenticatedContext auth)
    {
        var restaurantId = auth.RestaurantId;
        var restaurant = await _restaurantsRepository.GetByIdAsync(restaurantId);
        if (restaurant == null)
        {
            return NotFound();
        }

        return Ok(restaurant);
    }

    [HttpPost("CreateRestaurant")]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand createRestaurantCommand)
    {
        var restaurantId = createRestaurantCommand.RestaurantId;
        var newRestaurant = await _createRestaurantCommandHandler.HandleAsync(createRestaurantCommand, restaurantId);
        
        return Ok();
    }

    [HttpPut("UpdateRestaurant/{id}")]
    public async Task<IActionResult> UpdateRestaurant(
        [FromRoute] int id,
        [FromServices] AuthenticatedContext auth,
        [FromBody] UpdateRestaurantCommand updateRestaurantCommand)
    {
        var restaurantId = auth.RestaurantId;
        updateRestaurantCommand.Id = id;
        Restaurants updateRestaurant = new();

        var existingRestaurant = await _restaurantsRepository.GetByIdAsync(restaurantId);
        if (existingRestaurant != null)
        {
            updateRestaurant = await _updateRestaurantCommandHandler.HandleAsync(updateRestaurantCommand, restaurantId);
        }
        else
        {
            return NotFound();
        }

        await _updateRestaurantCommandHandler.HandleAsync(updateRestaurantCommand, restaurantId);
        return Ok(updateRestaurant);
    }
}
