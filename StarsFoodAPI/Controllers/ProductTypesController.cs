using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Application.Models;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

[Route("api")]
[ApiController]
public class ProductTypesController : ControllerBase
{
    private readonly IProductTypesRepository _productTypesRepository;

    public ProductTypesController(IProductTypesRepository productTypesRepository)
    {
        _productTypesRepository = productTypesRepository;
    }

    [HttpGet("GetAllProductTypes")]
    public async Task<IActionResult> GetAllProductTypes(int restaurantId)
    {
        var productType = await _productTypesRepository.GetAllAsync(restaurantId);
        return Ok(productType);
    }

    [HttpGet("GetProductType/{id}")]
    public async Task<IActionResult> GetProductTypeById(int id)
    {
        var productType = await _productTypesRepository.GetByIdAsync(id);
        if (productType == null)
        {
            return NotFound();
        }

        return Ok(productType);
    }

    [HttpPost("CreateProductType")]
    public async Task<IActionResult> CreateProductType([FromBody] ProductTypesModel productTypesModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var newProductType = new ProductTypes
        {
            TypeName = productTypesModel.Name,
            RestaurantId = productTypesModel.RestaurantId
        };

        await _productTypesRepository.CreateAsync(newProductType);
        return Ok(newProductType);
    }

    [HttpPut("UpdateProductType/{id}")]
    public async Task<IActionResult> UpdateProductType(int id, [FromBody] ProductTypes productTypes)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingProductType = await _productTypesRepository.GetByIdAsync(id);
        if (existingProductType == null)
        {
            return NotFound();
        }

        existingProductType.Update(productTypes.TypeName);

        await _productTypesRepository.UpdateAsync(existingProductType);
        return Ok(existingProductType);
    }

    [HttpPut("SetProductTypeAvailability/{id}")]
    public async Task<IActionResult> SetAvailability(int id, bool isAvailable)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingProductType = await _productTypesRepository.GetByIdAsync(id);
        if (existingProductType == null)
        {
            return NotFound();
        }

        existingProductType.SetAvailability(isAvailable);
        return Ok(existingProductType);
    }

    [HttpDelete("DeleteProductType/{id}")]
    public async Task<IActionResult> DeleteProductType(int id)
    {
        var productType = await _productTypesRepository.GetByIdAsync(id);
        if (productType == null)
        {
            return NotFound();
        }

        await _productTypesRepository.DeleteAsync(id);
        return Ok();
    }
}

