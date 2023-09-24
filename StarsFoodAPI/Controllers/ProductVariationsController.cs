using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Models;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

[Route("api")]
[ApiController]
public class ProductVariationsController : ControllerBase
{
    private readonly IProductVariationsRepository _productVariationsRepository;

    public ProductVariationsController(IProductVariationsRepository productVariationsRepository)
    {
        _productVariationsRepository = productVariationsRepository;
    }

    [HttpGet("GetAllVariations")]
    public async Task<IActionResult> GetAllVariations(int restaurantId)
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
    public async Task<IActionResult> CreateVariation([FromBody] ProductVariationsModel variationModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newVariation = new ProductVariations
        {
            Description = variationModel.Description,
            RestaurantId = variationModel.RestaurantId
        };

        await _productVariationsRepository.CreateAsync(newVariation);
        return Ok(newVariation);
    }

    [HttpPut("UpdateVariation/{id}")]
    public async Task<IActionResult> UpdateVariation(int id, [FromBody] ProductVariations variation)
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
        return Ok();
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
        return Ok();
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
