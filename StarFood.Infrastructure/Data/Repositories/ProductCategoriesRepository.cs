using Microsoft.EntityFrameworkCore;
using StarFood.Application.Interfaces;
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

        public async Task<ProductCategories> GetByIdAsync(int Id)
        {
            return await _context.Categories.FindAsync(Id);
        }

        public async Task CreateAsync(ProductCategories category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ProductCategories category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var category = await GetByIdAsync(Id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ChangeAvailability(int id, bool isAvailable)
        {
            var category = _context.Productes.Find(id);

            if (category != null)
            {
                category.SetAvailability(isAvailable);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Categoria não encontrada");
            }
        }
    }
}
