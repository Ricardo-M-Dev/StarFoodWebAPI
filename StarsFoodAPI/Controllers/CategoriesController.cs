using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Application.Models;
using StarFood.Domain.Entities;

[Route("api")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesRepository _categoriesRepository;
    
    public CategoriesController(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    [HttpGet("GetAllCategories")]
    public async Task<IActionResult> GetAllCategories(int restaurantId)
    {
        var categories = await _categoriesRepository.GetAllAsync(restaurantId);
        return Ok(categories);
    }

    [HttpGet("GetCategory/{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoriesRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpPost("CreateCategory")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoriesModel categoryModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newCategory = new Categories
        {
            CategoryName = categoryModel.Name,
            RestaurantId = categoryModel.RestaurantId
        };
        
        await _categoriesRepository.CreateAsync(newCategory);
        return Ok(newCategory);
    }

    [HttpPut("UpdateCategory/{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] Categories category)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingCategory = await _categoriesRepository.GetByIdAsync(id);
        if (existingCategory == null)
        {
            return NotFound();
        }

        existingCategory.Update(category.CategoryName);

        await _categoriesRepository.UpdateAsync(id, existingCategory);
        return Ok();
    }

    [HttpPut("SetCategoryAvailability/{id}")]
    public async Task<IActionResult> SetAvailability(int id, bool isAvailable)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingCategory = await _categoriesRepository.GetByIdAsync(id);
        if (existingCategory == null)
        {
            return NotFound();
        }

        existingCategory.SetAvailability(isAvailable);
        return Ok();
    }

    [HttpDelete("DeleteCategory/{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _categoriesRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        await _categoriesRepository.DeleteAsync(id);
        return Ok();
    }
}
