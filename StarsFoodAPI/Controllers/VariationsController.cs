using Microsoft.AspNetCore.Mvc;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

[Route("api")]
[ApiController]
public class VariationsController : ControllerBase
{
    private readonly IVariationsRepository _productVariationsRepository;

    public VariationsController(IVariationsRepository productVariationsRepository)
    {
        _productVariationsRepository = productVariationsRepository;
    }

    [HttpGet("GetAllVariations")]
    public async Task<IActionResult> GetAllVariations(string restaurantId)
    {
        var variations = await _productVariationsRepository.GetAllAsync(restaurantId);
        return Ok(variations);
    }

    [HttpGet("GetVariation/{id}")]
    public async Task<IActionResult> GetVariationById(int id)
    {
        var variation = await _productVariationsRepository.GetByIdAsync(id);
        if (variation == null)
        {
            return NotFound();
        }

        return Ok(variation);
    }

    [HttpPost("CreateVariation")]
    public async Task<IActionResult> CreateVariation([FromBody] CreateVariationCommand variationCommand)
    {
        var newVariation = new Variations
        {
            Description = variationCommand.Description,
            Value = variationCommand.Value,
            RestaurantId = variationCommand.RestaurantId,
        };

        if (newVariation != null)
        {
            await _productVariationsRepository.CreateAsync(newVariation);
            return Ok(newVariation);
        }
        else
        {
            return BadRequest();
        }

    }

    [HttpPut("UpdateVariation/{id}")]
    public async Task<IActionResult> UpdateVariation(int id, [FromBody] Variations variation)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingVariation = await _productVariationsRepository.GetByIdAsync(id);
        if (existingVariation == null)
        {
            return NotFound();
        }

        existingVariation.Update(variation.Description, variation.Value);

        await _productVariationsRepository.UpdateAsync(id, existingVariation);
        return Ok(existingVariation);
    }

    [HttpPut("SetVariationAvailability/{id}")]
    public async Task<IActionResult> SetAvailability(int id, bool isAvailable)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingVariation = await _productVariationsRepository.GetByIdAsync(id);
        if (existingVariation == null)
        {
            return NotFound();
        }

        existingVariation.SetAvailability(isAvailable);
        return Ok(existingVariation);
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
