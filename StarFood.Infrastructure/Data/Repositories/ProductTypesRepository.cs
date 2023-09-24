using Microsoft.EntityFrameworkCore;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class ProductTypesRepository : IProductTypesRepository
    {
        private readonly StarFoodDbContext _context;

        public ProductTypesRepository(StarFoodDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductTypes>> GetAllAsync(int restaurantId)
        {
            return await _context.ProductTypes
                .Where(pt => pt.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<ProductTypes> GetByIdAsync(int Id)
        {
            return await _context.ProductTypes.FindAsync(Id);
        }

        public async Task CreateAsync(ProductTypes productType)
        {
            _context.ProductTypes.Add(productType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductTypes productType)
        {
            _context.ProductTypes.Update(productType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var productType = await GetByIdAsync(Id);
            if (productType != null)
            {
                _context.ProductTypes.Remove(productType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ChangeAvailability(int id, bool isAvailable)
        {
            var productType = _context.Dishes.Find(id);

            if (productType != null)
            {
                productType.SetAvailability(isAvailable);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Tipo não encontrado");
            }
        }
    }
}
