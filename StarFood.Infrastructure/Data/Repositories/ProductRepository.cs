using Microsoft.EntityFrameworkCore;
using StarFood.Application.Interfaces;
using StarFood.Domain.Entities;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StarFoodDbContext _context;

        public ProductRepository(StarFoodDbContext context)
        {
            _context = context;
        }

        public async Task<List<Products>> GetAllAsync(int restaurantId)
        {
            return await _context.Products
                .Where(d => d.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<Products> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Products>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                .Where(d => d.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task CreateAsync(Products product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int Id, Products updateProduct)
        {
            _context.Products.Update(updateProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
