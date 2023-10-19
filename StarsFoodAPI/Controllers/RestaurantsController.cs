using Microsoft.AspNetCore.Mvc;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

[Route("api")]
[ApiController]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantsRepository _restaurantsRepository;

    public RestaurantsController(IRestaurantsRepository restaurantsRepository)
    {
        _restaurantsRepository = restaurantsRepository;
    }

    [HttpGet("GetAllRestaurants")]
    public async Task<IActionResult> GetAllRestaurants()
    {
        var restaurants = await _restaurantsRepository.GetAllAsync();
        return Ok(restaurants);
    }

    [HttpGet("GetRestaurant/{id}")]
    public async Task<IActionResult> GetRestaurantById(int id)
    {
        var restaurant = await _restaurantsRepository.GetByIdAsync(id);
        if (restaurant == null)
        {
            return NotFound();
        }

        return Ok(restaurant);
    }

    [HttpPost("CreateRestaurant")]
    public async Task<IActionResult> CreateRestaurant(int restaurantId, [FromBody] CreateRestaurantCommand createRestaurantCommand)
    {
        var newRestaurant = new Restaurants
        {
            Id = restaurantId,
            Name = createRestaurantCommand.Name,
        };

        await _restaurantsRepository.CreateAsync(newRestaurant);
        return Ok(newRestaurant);
    }

    //[HttpPut("UpdateRestaurant/{id}")]
    //public async Task<IActionResult> UpdateRestaurant(string restaurantId, [FromBody] Restaurants restaurant)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }

    //    var existingRestaurant = await _restaurantsRepository.GetByIdAsync(restaurantId);
    //    if (existingRestaurant == null)
    //    {
    //        return NotFound();
    //    }

    //    existingRestaurant.Update(restaurant.Name);

    //    await _restaurantsRepository.UpdateAsync(id, existingRestaurant);
    //    return Ok(existingRestaurant);
    //}

    [HttpPut("SetRestaurantAvailability/{id}")]
    public async Task<IActionResult> SetAvailability(int id, bool isAvailable)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingRestaurant = await _restaurantsRepository.GetByIdAsync(id);
        if (existingRestaurant == null)
        {
            return NotFound();
        }

        existingRestaurant.SetAvailability(isAvailable);
        return Ok(existingRestaurant);
    }

    [HttpDelete("DeleteRestaurant/{id}")]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
        var restaurant = await _restaurantsRepository.GetByIdAsync(id);
        if (restaurant == null)
        {
            return NotFound();
        }

        await _restaurantsRepository.DeleteAsync(id);
        return Ok();
    }
}
