using Microsoft.EntityFrameworkCore;
using StarFood.Application.Interfaces;
using StarFood.Domain.Entities;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class DishesRepository : IDishesRepository
    {
        private readonly StarFoodDbContext _context;

        public DishesRepository(StarFoodDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dishes>> GetAllAsync(int restaurantId)
        {
            return await _context.Dishes
                .Where(d => d.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<Dishes> GetByIdAsync(int id)
        {
            return await _context.Dishes.FindAsync(id);
        }

        public async Task<List<Dishes>> GetByType(int productTypeId)
        {
            return await _context.Dishes
                .Where(d => d.ProductTypeId == productTypeId)
                .ToListAsync();
        }
        public async Task<List<Dishes>> GetByCategory(int categoryId)
        {
            return await _context.Dishes
                .Where(d => d.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task CreateAsync(Dishes dish)
        {
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int Id, Dishes updateDish)
        {
            _context.Dishes.Update(updateDish);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeAvailability(int dishId, bool isAvailable)
        {
            var dish = _context.Dishes.Find(dishId);

            if (dish != null)
            {
                dish.SetAvailability(isAvailable);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Prato não encontrado");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var dish = await GetByIdAsync(id);
            if (dish != null)
            {
                _context.Dishes.Remove(dish);
                await _context.SaveChangesAsync();
            }
        }
    }
}
