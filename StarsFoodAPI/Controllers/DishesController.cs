using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Application.Models;
using StarFood.Domain.Entities;

[Route("api")]
[ApiController]
public class DishesController : ControllerBase
{
    private readonly IDishesRepository _dishesRepository;

    public DishesController(IDishesRepository dishesRepository, ICategoriesRepository categoryRepository)
    {
        _dishesRepository = dishesRepository;
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
    public async Task<IActionResult> CreateDish([FromBody] DishesModel dishModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newDish = new Dishes
        {
            Name = dishModel.Name,
            Description = dishModel.Description,
            ProductTypeId = dishModel.ProductTypeId,
            CategoryId = dishModel.CategoryId,
            RestaurantId = dishModel.RestaurantId,
        };

        await _dishesRepository.CreateAsync(newDish);
        return Ok(newDish);
    }

    [HttpPut("UpdateDish/{id}")]
    public async Task<IActionResult> UpdateDish(int id, [FromBody] Dishes dish)
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

        existingDish.Update(dish.Name, dish.Description, dish.ProductTypeId, dish.CategoryId, dish.IsAvailable);

        await _dishesRepository.UpdateAsync(id, existingDish);
        return Ok(existingDish);
    }

    [HttpPut("SetDishAvailability/{id}")]
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
