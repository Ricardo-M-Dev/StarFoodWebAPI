using Microsoft.EntityFrameworkCore;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class RestaurantsRepository : IRestaurantsRepository
    {
        private readonly StarFoodDbContext _context;

        public RestaurantsRepository(StarFoodDbContext context)
        {
            _context = context;
        }

        public async Task<List<Restaurants>> GetAllAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurants> GetByIdAsync(int id)
        {
            return await _context.Restaurants.FindAsync(id);
        }

        public async Task CreateAsync(Restaurants restaurant)
        {
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Restaurants updatedRestaurant)
        {
            _context.Restaurants.Update(updatedRestaurant);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeAvailability(int id, bool isAvailable)
        {
            var restaurant = _context.Restaurants.Find(id);

            if (restaurant != null)
            {
                restaurant.SetAvailability(isAvailable);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var restaurant = await GetByIdAsync(id);

            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
            }
        }
    }
}
