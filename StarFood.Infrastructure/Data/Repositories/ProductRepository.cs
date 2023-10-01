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
            return await _context.Productes
                .Where(d => d.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<Products> GetByIdAsync(int id)
        {
            return await _context.Productes.FindAsync(id);
        }

        public async Task<List<Products>> GetByType(int productTypeId)
        {
            return await _context.Productes
                .Where(d => d.ProductTypeId == productTypeId)
                .ToListAsync();
        }
        public async Task<List<Products>> GetByCategory(int categoryId)
        {
            return await _context.Productes
                .Where(d => d.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task CreateAsync(Products product)
        {
            _context.Productes.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int Id, Products updateProduct)
        {
            _context.Productes.Update(updateProduct);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeAvailability(int productId, bool isAvailable)
        {
            var product = _context.Productes.Find(productId);

            if (product != null)
            {
                product.SetAvailability(isAvailable);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Prato não encontrado");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Productes.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
