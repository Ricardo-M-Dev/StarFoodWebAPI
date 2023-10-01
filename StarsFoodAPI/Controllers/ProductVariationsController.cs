using Microsoft.AspNetCore.Mvc;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarsFoodAPI.Controllers
{
    public class ProductVariationsController : ControllerBase
    {
        private readonly IProductVariationsRepository _productVariationsRepository;

        public ProductVariationsController(IProductVariationsRepository productVariationsRepository)
        {
            _productVariationsRepository = productVariationsRepository;
        }

        [HttpGet("GetAllProductsVariations")]
        public async Task<IActionResult> GetAllProductVariations(int restaurantId)
        {
            var productVariation = await _productVariationsRepository.GetAllAsync(restaurantId);
            return Ok(productVariation);
        }

        [HttpGet("GetProductVariation/{id}")]
        public async Task<IActionResult> GetProductVariation(int id)
        {
            var productVariation = await _productVariationsRepository.GetByIdAsync(id);
            if (productVariation == null)
            {
                return NotFound();
            }

            return Ok(productVariation);
        }

        [HttpGet("GetProductVariationByProductId/{id}")]
        public async Task<IActionResult> GetProductVariationByProductId(int productId)
        {
            var productVariation = await _productVariationsRepository.GetByProductId(productId);
            if (productVariation == null)
            {
                return NotFound();
            }

            return Ok(productVariation);
        }

        [HttpGet("GetProductVariationByVariationId/{id}")]
        public async Task<IActionResult> GetProductVariationByVariationId(int productVariationId)
        {
            var productVariation = await _productVariationsRepository.GetByVariationId(productVariationId);
            if (productVariation == null)
            {
                return NotFound();
            }

            return Ok(productVariation);
        }

        //[HttpPost("CreateProductVariation")]
        //public async Task<IActionResult> CreateProductVariation([FromBody] ProductsProductVariationsModel productsVariationsModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var newProductVariation = new ProductVariations
        //    {
        //        ProductId = productsVariationsModel.ProductId,
        //        VariationId = productsVariationsModel.VariationId,
        //        RestaurantId = productsVariationsModel.RestaurantId
        //    };

        //    await _productVariationsRepository.CreateAsync(newProductVariation);
        //    return Ok(newProductVariation);
        //}

        [HttpPut("UpdateProductVariation/{id}")]
        public async Task<IActionResult> UpdateProductVariation(int id, [FromBody] ProductVariations variation)
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

            existingVariation.Update(variation.ProductId, variation.VariationId);

            await _productVariationsRepository.UpdateAsync(id, existingVariation);
            return Ok(existingVariation);
        }

        [HttpDelete("DeleteProductVariation/{id}")]
        public async Task<IActionResult> DeleteProductVariation(int id)
        {
            var category = await _productVariationsRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _productVariationsRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
