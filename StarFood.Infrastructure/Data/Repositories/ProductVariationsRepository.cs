using Microsoft.EntityFrameworkCore;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class ProductVariationsRepository : IProductVariationsRepository
    {
        private readonly StarFoodDbContext _context;

        public ProductVariationsRepository(StarFoodDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductVariations>> GetAllAsync(int restaurantId)
        {
            return await _context.ProductesProductVariations
                .Where(dp => dp.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<ProductVariations> GetByIdAsync(int id)
        {
            return await _context.ProductesProductVariations.FindAsync(id);
        }

        public async Task<List<ProductVariations>> GetByProductId(int productId)
        {
            return await _context.ProductesProductVariations
                .Where(dp => dp.ProductId == productId)
                .ToListAsync();
        }

        public async Task<List<ProductVariations>> GetByVariationId(int productVariationId)
        {
            return await _context.ProductesProductVariations
                .Where(dp => dp.VariationId == productVariationId)
                .ToListAsync();
        }

        public async Task CreateAsync(ProductVariations productesProductVariations)
        {
            _context.ProductesProductVariations.Add(productesProductVariations);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ProductVariations productesProductVariations)
        {
            _context.ProductesProductVariations.Update(productesProductVariations);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeAvailability(int id, bool isAvailable)
        {
            var productProductVariation = _context.Productes.Find(id);

            if (productProductVariation != null)
            {
                productProductVariation.SetAvailability(isAvailable);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Variação não encontrado");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var productProductVariation = await GetByIdAsync(id);
            if (productProductVariation != null)
            {
                _context.ProductesProductVariations.Remove(productProductVariation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
