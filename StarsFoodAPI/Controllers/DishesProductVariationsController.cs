using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Application.Models;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarsFoodAPI.Controllers
{
    public class DishesProductVariationsController : ControllerBase
    {
        private readonly IDishesProductVariationsRepository _dishesProductVariationsRepository;

        public DishesProductVariationsController(IDishesProductVariationsRepository dishesProductVariationsRepository)
        {
            _dishesProductVariationsRepository = dishesProductVariationsRepository;
        }

        [HttpGet("GetAllDishesVariations")]
        public async Task<IActionResult> GetAllDishVariations(int restaurantId)
        {
            var dishVariation = await _dishesProductVariationsRepository.GetAllAsync(restaurantId);
            return Ok(dishVariation);
        }

        [HttpGet("GetDishVariation/{id}")]
        public async Task<IActionResult> GetDishVariation(int id)
        {
            var dishVariation = await _dishesProductVariationsRepository.GetByIdAsync(id);
            if (dishVariation == null)
            {
                return NotFound();
            }

            return Ok(dishVariation);
        }

        [HttpGet("GetDishVariationByDishId/{id}")]
        public async Task<IActionResult> GetDishVariationByDishId(int dishId)
        {
            var dishVariation = await _dishesProductVariationsRepository.GetByDishId(dishId);
            if (dishVariation == null)
            {
                return NotFound();
            }

            return Ok(dishVariation);
        }

        [HttpGet("GetDishVariationByProductVariationId/{id}")]
        public async Task<IActionResult> GetDishVariationByProductVariationId(int productVariationId)
        {
            var dishVariation = await _dishesProductVariationsRepository.GetByProductVariationId(productVariationId);
            if (dishVariation == null)
            {
                return NotFound();
            }

            return Ok(dishVariation);
        }

        [HttpPost("CreateDishVariation")]
        public async Task<IActionResult> CreateDishVariation([FromBody] DishesProductVariationsModel dishesVariationsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newDishVariation = new DishesProductVariations
            {
                DishesId = dishesVariationsModel.DishId,
                ProductVariationId = dishesVariationsModel.VariationId,
                RestaurantId = dishesVariationsModel.RestaurantId
            };

            await _dishesProductVariationsRepository.CreateAsync(newDishVariation);
            return Ok(newDishVariation);
        }

        [HttpPut("UpdateDishVariation/{id}")]
        public async Task<IActionResult> UpdateDishVariation(int id, [FromBody] DishesProductVariations variation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingVariation = await _dishesProductVariationsRepository.GetByIdAsync(id);
            if (existingVariation == null)
            {
                return NotFound();
            }

            existingVariation.Update(variation.DishesId, variation.ProductVariationId);

            await _dishesProductVariationsRepository.UpdateAsync(id, existingVariation);
            return Ok();
        }

        [HttpDelete("DeleteDishVariation/{id}")]
        public async Task<IActionResult> DeleteDishVariation(int id)
        {
            var category = await _dishesProductVariationsRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _dishesProductVariationsRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
