using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Application.Models;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarsFoodAPI.Controllers
{
    public class ProductVariationsController : ControllerBase
    {
        private readonly IProductVariationsRepository _productesProductVariationsRepository;

        public ProductVariationsController(IProductVariationsRepository productesProductVariationsRepository)
        {
            _productesProductVariationsRepository = productesProductVariationsRepository;
        }

        [HttpGet("GetAllProductesVariations")]
        public async Task<IActionResult> GetAllProductVariations(int restaurantId)
        {
            var productVariation = await _productesProductVariationsRepository.GetAllAsync(restaurantId);
            return Ok(productVariation);
        }

        [HttpGet("GetProductVariation/{id}")]
        public async Task<IActionResult> GetProductVariation(int id)
        {
            var productVariation = await _productesProductVariationsRepository.GetByIdAsync(id);
            if (productVariation == null)
            {
                return NotFound();
            }

            return Ok(productVariation);
        }

        [HttpGet("GetProductVariationByProductId/{id}")]
        public async Task<IActionResult> GetProductVariationByProductId(int productId)
        {
            var productVariation = await _productesProductVariationsRepository.GetByProductId(productId);
            if (productVariation == null)
            {
                return NotFound();
            }

            return Ok(productVariation);
        }

        [HttpGet("GetProductVariationByVariationId/{id}")]
        public async Task<IActionResult> GetProductVariationByVariationId(int productVariationId)
        {
            var productVariation = await _productesProductVariationsRepository.GetByVariationId(productVariationId);
            if (productVariation == null)
            {
                return NotFound();
            }

            return Ok(productVariation);
        }

        [HttpPost("CreateProductVariation")]
        public async Task<IActionResult> CreateProductVariation([FromBody] ProductesProductVariationsModel productesVariationsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newProductVariation = new ProductVariations
            {
                ProductesId = productesVariationsModel.ProductId,
                VariationId = productesVariationsModel.VariationId,
                RestaurantId = productesVariationsModel.RestaurantId
            };

            await _productesProductVariationsRepository.CreateAsync(newProductVariation);
            return Ok(newProductVariation);
        }

        [HttpPut("UpdateProductVariation/{id}")]
        public async Task<IActionResult> UpdateProductVariation(int id, [FromBody] ProductVariations variation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingVariation = await _productesProductVariationsRepository.GetByIdAsync(id);
            if (existingVariation == null)
            {
                return NotFound();
            }

            existingVariation.Update(variation.ProductesId, variation.VariationId);

            await _productesProductVariationsRepository.UpdateAsync(id, existingVariation);
            return Ok(existingVariation);
        }

        [HttpDelete("DeleteProductVariation/{id}")]
        public async Task<IActionResult> DeleteProductVariation(int id)
        {
            var category = await _productesProductVariationsRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _productesProductVariationsRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
