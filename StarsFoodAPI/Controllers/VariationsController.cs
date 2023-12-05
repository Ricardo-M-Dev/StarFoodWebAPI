using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;
using StarsFoodAPI.Services.HttpContext;

[Authorize]
[Route("api")]
[ApiController]
public class VariationsController : ControllerBase
{
    private readonly IVariationsRepository _productVariationsRepository;
    private readonly ICommandHandler<CreateVariationCommand, Variations> _createVariationCommandHandler;
    private readonly ICommandHandler<UpdateVariationCommand, Variations> _updateVariationCommandHandler;

    public VariationsController(
        IVariationsRepository productVariationsRepository, 
        ICommandHandler<CreateVariationCommand, Variations> createVariationCommandHandler,
        ICommandHandler<UpdateVariationCommand, Variations> updateVariationCommandHandler)
    {
        _productVariationsRepository = productVariationsRepository;
        _createVariationCommandHandler = createVariationCommandHandler;
        _updateVariationCommandHandler = updateVariationCommandHandler;
    }

    [HttpGet("GetAllVariations")]
    public async Task<IActionResult> GetAllVariations([FromServices] RequestState auth)
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
    public async Task<IActionResult> GetVariationById([FromServices]RequestState auth, int id)
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
        [FromServices] RequestState auth,
        [FromBody] List<CreateVariationCommand> createVariationCommand)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var newVariation = await _createVariationCommandHandler.HandleAsyncList(createVariationCommand, restaurantId);

            if (newVariation != null)
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

    [HttpPatch("UpdateVariations")]
    public async Task<IActionResult> UpdateVariations(
        [FromServices] RequestState auth,
        [FromBody] List<UpdateVariationCommand> updateVariationCommand)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var updatedVariation = await _updateVariationCommandHandler.HandleAsyncList(updateVariationCommand, restaurantId);

            if (updatedVariation != null)
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

    [HttpDelete("DeleteVariation/{id}")]
    public async Task<IActionResult> DeleteVariation(int id)
    {
        try
        {
            var variation = await _productVariationsRepository.GetByIdAsync(id);
            if (variation == null)
            {
                return NotFound();
            }

            await _productVariationsRepository.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
        
    }
}
