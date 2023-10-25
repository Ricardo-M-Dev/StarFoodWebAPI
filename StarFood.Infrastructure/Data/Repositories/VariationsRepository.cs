using Microsoft.EntityFrameworkCore;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class VariationsRepository : IVariationsRepository
    {
        private readonly StarFoodDbContext _context;

        public VariationsRepository(StarFoodDbContext context)
        {
            _context = context;
        }

        public async Task<List<Variations>> GetAllAsync(int restaurantId)
        {
            return await _context.Variations
                .Where(pv => pv.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<Variations> GetByIdAsync(int id)
        {
            return await _context.Variations.FindAsync(id);
        }

        public async Task<List<Variations>> GetByProductIdAsync(int productId)
        {
            return await _context.Variations
                .Where(v => v.ProductId == productId)
                .ToListAsync();
        }

        public async Task CreateAsync(Variations productVariation)
        {
            await _context.Variations.AddAsync(productVariation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Variations productVariation)
        {
            _context.Variations.Update(productVariation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productVariation = await GetByIdAsync(id);
            if (productVariation != null)
            {
                _context.Variations.Remove(productVariation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
