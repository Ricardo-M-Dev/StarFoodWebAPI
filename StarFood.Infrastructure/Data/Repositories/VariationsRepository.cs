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
            return await _context.ProductVariations
                .Where(pv => pv.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<Variations> GetByIdAsync(int id)
        {
            return await _context.ProductVariations.FindAsync(id);
        }

        public async Task CreateAsync(Variations productVariation)
        {
            _context.ProductVariations.Add(productVariation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Variations productVariation)
        {
            _context.ProductVariations.Update(productVariation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productVariation = await GetByIdAsync(id);
            if (productVariation != null)
            {
                _context.ProductVariations.Remove(productVariation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ChangeAvailability(int id, bool isAvailable)
        {
            var productVariation = _context.Productes.Find(id);

            if (productVariation != null)
            {
                productVariation.SetAvailability(isAvailable);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Variação não encontrada");
            }
        }
    }

}
