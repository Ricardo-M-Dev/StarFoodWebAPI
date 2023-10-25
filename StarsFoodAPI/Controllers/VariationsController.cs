using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;
using StarsFoodAPI.Services.HttpContext;

[Route("api")]
[ApiController]
public class VariationsController : ControllerBase
{
    private readonly StarFoodDbContext _context;
    private readonly IVariationsRepository _productVariationsRepository;
    private readonly ICommandHandler<CreateVariationCommand, Variations> _createVariationCommandHandler;
    private readonly ICommandHandler<UpdateVariationCommand, Variations> _updateVariationCommandHandler;

    public VariationsController(StarFoodDbContext context,
                                IVariationsRepository productVariationsRepository, 
                                ICommandHandler<CreateVariationCommand, Variations> createVariationCommandHandler,
                                ICommandHandler<UpdateVariationCommand, Variations> updateVariationCommandHandler)
    {
        _context = context;
        _productVariationsRepository = productVariationsRepository;
        _createVariationCommandHandler = createVariationCommandHandler;
        _updateVariationCommandHandler = updateVariationCommandHandler;
    }

    [HttpGet("GetAllVariations")]
    public async Task<IActionResult> GetAllVariations([FromServices] AuthenticatedContext auth)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var variations = await _productVariationsRepository.GetAllAsync(restaurantId);
            return Ok(variations);
        }
        catch (Exception ex) 
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpPut("GetVariation/{id}")]
    public async Task<IActionResult> GetVariationById([FromServices]AuthenticatedContext auth, int id)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var variation = await _productVariationsRepository.GetByIdAsync(id);
            if (variation == null)
            {
                return NotFound();
            }

            return Ok(variation);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpPost("CreateVariation")]
    public async Task<IActionResult> CreateVariation(
        [FromServices] AuthenticatedContext auth,
        [FromBody] List<CreateVariationCommand> createVariationCommand)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var newVariation = await _createVariationCommandHandler.HandleAsyncList(createVariationCommand, restaurantId);

            if (newVariation != null)
            {
                return Ok(newVariation);
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

    [HttpPatch("UpdateVariation/{id}")]
    public async Task<IActionResult> UpdateVariation(
        [FromServices] AuthenticatedContext auth,
        [FromBody] UpdateVariationCommand updateVariationCommand)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            Variations updateVariation = new();
            var variation = _context.Variations.FindAsync(updateVariationCommand.Id);

            if (variation.Result != null)
            {
                updateVariation = await _updateVariationCommandHandler.HandleAsync(updateVariationCommand, restaurantId);
            }
            else
            {
                return NotFound();
            }

            return Ok(updateVariation);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }

    }

    [HttpDelete("DeleteVariation/{id}")]
    public async Task<IActionResult> DeleteVariation(int id)
    {
        var variation = await _productVariationsRepository.GetByIdAsync(id);
        if (variation == null)
        {
            return NotFound();
        }

        await _productVariationsRepository.DeleteAsync(id);
        return Ok();
    }
}
