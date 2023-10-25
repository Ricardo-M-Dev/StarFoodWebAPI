using Microsoft.EntityFrameworkCore;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly StarFoodDbContext _context;

        public CategoriesRepository(StarFoodDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categories>> GetAllAsync(int restaurantId)
        {
            return await _context.Categories
                .Where(c => c.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<Categories> GetByIdAsync(int id, int restaurantId)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.RestaurantId == restaurantId);
        }

        public async Task CreateAsync(Categories category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Categories category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id, int restaurantId)
        {
            var category = await GetByIdAsync(Id, restaurantId);

            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
