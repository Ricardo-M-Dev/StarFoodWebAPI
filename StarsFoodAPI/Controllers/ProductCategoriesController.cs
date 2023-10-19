using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Infrastructure.Data;
using StarFood.Infrastructure.Data.Repositories;
using StarsFoodAPI.Services.HttpContext;
using System.Numerics;

[Route("api")]
[ApiController]
public class ProductCategoriesController : ControllerBase
{
    private readonly StarFoodDbContext _context;
    private readonly IProductCategoriesRepository _productCategoriesRepository;
    private readonly ICommandHandler<UpdateProductCategoryCommand, ProductCategories> _updateCategoryCommandHandler;
    private readonly ICommandHandler<CreateProductCategoryCommand, ProductCategories> _createCategoryCommandHandler;
    
    public ProductCategoriesController(
        StarFoodDbContext context,
        IProductCategoriesRepository productCategoriesRepository,
        ICommandHandler<UpdateProductCategoryCommand, ProductCategories> updateCategoriesRepository, 
        ICommandHandler<CreateProductCategoryCommand, ProductCategories> createCategoryCommandHandler)
    {
        _context = context;
        _productCategoriesRepository = productCategoriesRepository;
        _updateCategoryCommandHandler = updateCategoriesRepository;
        _createCategoryCommandHandler = createCategoryCommandHandler;
    }

    [HttpGet("GetAllCategories")]
    public async Task<IActionResult> GetAllCategories([FromServices] AuthenticatedContext auth)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var categories = await _productCategoriesRepository.GetAllAsync(restaurantId);

            if (categories == null)
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
        [FromServices] AuthenticatedContext auth,
        int id)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var category = await _productCategoriesRepository.GetByIdAsync(id, restaurantId);

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
        [FromBody] CreateProductCategoryCommand createCategoryCommand)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var newCategory = await _createCategoryCommandHandler.HandleAsync(createCategoryCommand, restaurantId);

            if (newCategory != null)
            {
                return Ok(newCategory);
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

    [HttpPut("UpdateCategory/{id}")]
    public async Task<IActionResult> UpdateCategory(
        [FromServices] AuthenticatedContext auth,
        [FromBody] UpdateProductCategoryCommand updateCategoryCommand,
        int id)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && c.RestaurantId == restaurantId);

            if (existingCategory == null)
            {
                return NotFound();
            }
            else
            {
                updateCategoryCommand.Id = id;
                await _updateCategoryCommandHandler.HandleAsync(updateCategoryCommand, restaurantId);
                return Ok();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    //[HttpDelete("DeleteCategory/{id}")]
    //public async Task<IActionResult> DeleteCategory(int id)
    //{
    //    var category = await _productCategoriesRepository.GetByIdAsync(id);
    //    if (category == null)
    //    {
    //        return NotFound();
    //    }

    //    await _categoriesRepository.DeleteAsync(id);
    //    return Ok();
    //}
}
