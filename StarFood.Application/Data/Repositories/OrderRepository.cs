using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StarFood.Domain;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrdersRepository
    {
        private readonly StarFoodDbContext _context;

        public OrderRepository(StarFoodDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Orders order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await GetByIdAsync(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Orders>> GetAllAsync(int restaurantId)
        {
            return await _context.Orders
                .Where(o => o.RestaurantId == restaurantId)
                .ToListAsync();
        }

        public async Task<Orders> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Orders> GetByTableAsync(int table)
        {
            return await _context.Orders
                .Where(o => o.Table == table && o.OrderDate == DateTime.Now)
                .FirstOrDefaultAsync();
        }

        public async Task<Orders> GetByWaiterNameAsync(string waiterName)
        {
            return await _context.Orders
                .Where(o => o.Waiter == waiterName && o.OrderDate == DateTime.Now)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int id, Orders updatedOrder)
        {
            _context.Orders.Update(updatedOrder);
            await _context.SaveChangesAsync();
        }
    }
}
