using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;

[Route("api")]
[ApiController]
public class DishesController : ControllerBase
{
    private readonly IDishesRepository _dishesRepository;
    private readonly ICommandHandler<CreateDishCommand, Dishes> _createDishCommandHandler;
    private readonly ICommandHandler<UpdateDishCommand, Dishes> _updateDishCommandHandler;

    public DishesController(IDishesRepository dishesRepository, ICommandHandler<CreateDishCommand, Dishes> createDishCommandHandler, ICommandHandler<UpdateDishCommand, Dishes> updateDishCommandHandler)
    {
        _dishesRepository = dishesRepository;
        _createDishCommandHandler = createDishCommandHandler;
        _updateDishCommandHandler = updateDishCommandHandler;
    }

    [HttpGet("GetAllDishes")]
    public async Task<IActionResult> GetAllDishes(int restaurantId)
    {
        var dishes = await _dishesRepository.GetAllAsync(restaurantId);
        return Ok(dishes);
    }

    [HttpGet("GetDish/{id}")]
    public async Task<IActionResult> GetDishById(int id)
    {
        var dish = await _dishesRepository.GetByIdAsync(id);
        if (dish == null)
        {
            return NotFound();
        }

        return Ok(dish);
    }

    [HttpPost("CreateDish")]
    public async Task<IActionResult> CreateDish([FromBody] CreateDishCommand createDishCommand)
    {
        try
        {
            var newDish = await _createDishCommandHandler.HandleAsync(createDishCommand);

            if (newDish != null)
            {
                return Ok(newDish);
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

    [HttpPatch("UpdateDish/{id}")]
    public async Task<IActionResult> UpdateDish(int id, [FromBody] UpdateDishCommand updateDishCommand)
    {
        try
        {
            var updatedDish = await _updateDishCommandHandler.HandleAsync(updateDishCommand);

            if (updatedDish != null)
            {
                return Ok(updatedDish);
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

    [HttpPatch("SetDishAvailability/{id}")]
    public async Task<IActionResult> SetAvailability(int id, bool isAvailable)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingDish = await _dishesRepository.GetByIdAsync(id);
        if (existingDish == null)
        {
            return NotFound();
        }

        existingDish.SetAvailability(isAvailable);
        return Ok(existingDish);
    }

    [HttpDelete("DeleteDish/{id}")]
    public async Task<IActionResult> DeleteDish(int id)
    {
        var dish = await _dishesRepository.GetByIdAsync(id);
        if (dish == null)
        {
            return NotFound();
        }

        await _dishesRepository.DeleteAsync(id);
        return Ok();
    }
}
