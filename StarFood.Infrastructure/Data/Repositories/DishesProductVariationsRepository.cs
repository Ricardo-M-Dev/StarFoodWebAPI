using Microsoft.EntityFrameworkCore;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class DishesProductVariationsRepository : IDishesProductVariationsRepository
    {
        private readonly StarFoodDbContext _context;

        public DishesProductVariationsRepository(StarFoodDbContext context)
        {
            _context = context;
        }

        public async Task<List<DishesProductVariations>> GetAllAsync(int restaurantId)
        {
            return await _context.DishesProductVariations
                .Where(dp => dp.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<DishesProductVariations> GetByIdAsync(int id)
        {
            return await _context.DishesProductVariations.FindAsync(id);
        }

        public async Task<List<DishesProductVariations>> GetByDishId(int dishId)
        {
            return await _context.DishesProductVariations
                .Where(dp => dp.DishesId == dishId)
                .ToListAsync();
        }

        public async Task<List<DishesProductVariations>> GetByProductVariationId(int productVariationId)
        {
            return await _context.DishesProductVariations
                .Where(dp => dp.ProductVariationId == productVariationId)
                .ToListAsync();
        }

        public async Task CreateAsync(DishesProductVariations dishesProductVariations)
        {
            _context.DishesProductVariations.Add(dishesProductVariations);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, DishesProductVariations dishesProductVariations)
        {
            _context.DishesProductVariations.Update(dishesProductVariations);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeAvailability(int id, bool isAvailable)
        {
            var dishProductVariation = _context.Dishes.Find(id);

            if (dishProductVariation != null)
            {
                dishProductVariation.SetAvailability(isAvailable);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Variação não encontrado");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var dishProductVariation = await GetByIdAsync(id);
            if (dishProductVariation != null)
            {
                _context.DishesProductVariations.Remove(dishProductVariation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
