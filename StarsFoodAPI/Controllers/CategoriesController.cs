using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Infrastructure.Data;
using StarsFoodAPI.Services.HttpContext;

[Authorize]
[Route("api")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly StarFoodDbContext _context;
    private readonly IProductRepository _productRepository;
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly ICommandHandler<UpdateCategoryCommand, Categories> _updateCategoryCommandHandler;
    private readonly ICommandHandler<CreateCategoryCommand, Categories> _createCategoryCommandHandler;
    
    public CategoriesController(
        StarFoodDbContext context,
        IProductRepository productRepository,
        ICategoriesRepository productCategoriesRepository,
        ICommandHandler<UpdateCategoryCommand, Categories> updateCategoriesRepository, 
        ICommandHandler<CreateCategoryCommand, Categories> createCategoryCommandHandler)
    {
        _context = context;
        _productRepository = productRepository;
        _categoriesRepository = productCategoriesRepository;
        _updateCategoryCommandHandler = updateCategoriesRepository;
        _createCategoryCommandHandler = createCategoryCommandHandler;
    }

    [HttpGet("GetAllCategories")]
    public async Task<IActionResult> GetAllCategories([FromServices] AuthenticatedContext auth)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var categories = await _categoriesRepository.GetAllAsync(restaurantId);

            if (categories.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(categories);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpGet("GetCategory/{id}")]
    public async Task<IActionResult> GetCategoryById(
        [FromServices] AuthenticatedContext auth, int id)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var category = await _categoriesRepository.GetByIdAsync(id, restaurantId);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(category);
            }
        }
        catch   (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpPost("CreateCategory")]
    public async Task<IActionResult> CreateCategory(
        [FromServices] AuthenticatedContext auth,
        [FromBody] CreateCategoryCommand createCategoryCommand)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var newCategory = await _createCategoryCommandHandler.HandleAsync(createCategoryCommand, restaurantId);

            if (newCategory != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpPatch("UpdateCategory/{id}")]
    public async Task<IActionResult> UpdateCategory(
        [FromRoute] int id,
        [FromServices] AuthenticatedContext auth,
        [FromBody] UpdateCategoryCommand updateCategoryCommand
        )
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            updateCategoryCommand.Id = id;
            var updatedCategory = await _updateCategoryCommandHandler.HandleAsync(updateCategoryCommand, restaurantId);

            if (updatedCategory != null)
            {
                return Ok(updatedCategory);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpDelete("DeleteCategory/{id}")]
    public async Task<IActionResult> DeleteCategory([FromServices] AuthenticatedContext auth, int id)
    {
        var restaurantId = auth.RestaurantId;
        var products = await _productRepository.GetByCategoryIdAsync(id);

        if (products.Count == 0)
        {
            return StatusCode(StatusCodes.Status406NotAcceptable);
        }

        var category = await _categoriesRepository.GetByIdAsync(id, restaurantId);

        if (category == null)
        {
            return NotFound();
        }

        await _categoriesRepository.DeleteAsync(id, restaurantId);
        return Ok();
    }
}
