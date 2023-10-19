using Microsoft.EntityFrameworkCore;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class ProductCategoriesRepository : IProductCategoriesRepository
    {
        private readonly StarFoodDbContext _context;

        public ProductCategoriesRepository(StarFoodDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductCategories>> GetAllAsync(int restaurantId)
        {
            return await _context.Categories
                .Where(c => c.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<ProductCategories> GetByIdAsync(int id, int restaurantId)
        {
            return await _context.Categories
                .Where(c => c.Id == id && c.RestaurantId == restaurantId)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(ProductCategories category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductCategories category)
        {
            var existingCategory = _context.Categories.Local.FirstOrDefault(c => c.Id == category.Id);
            if (existingCategory != null)
            {
                _context.Categories.Update(existingCategory);
            }
            else
            {
                _context.Categories.Update(category);
            }
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
