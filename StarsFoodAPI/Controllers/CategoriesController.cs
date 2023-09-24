using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Application.Models;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;

[Route("api")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly ICommandHandler<CreateCategoryCommand, Categories> _createCategoryCommandHandler;
    
    public CategoriesController(ICategoriesRepository categoriesRepository, ICommandHandler<CreateCategoryCommand, Categories> createCategoryCOmmandHandler)
    {
        _categoriesRepository = categoriesRepository;
        _createCategoryCommandHandler = createCategoryCOmmandHandler;
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
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand createCategoryCommand)
    {
        try
        {
            var newCategory = await _createCategoryCommandHandler.HandleAsync(createCategoryCommand);

            if (newCategory != null)
            {
                return Ok(newCategory);
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
