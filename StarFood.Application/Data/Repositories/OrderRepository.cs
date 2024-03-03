using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StarFood.Domain;
using StarFood.Domain.Base;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class OrderRepository : BaseRepository<Orders>, IOrdersRepository
    {
        private readonly StarFoodDbContext _context;

        public OrderRepository(StarFoodDbContext context): base(context)
        {
            _context = context;
        }

        public Orders GetOrderById(int orderId, int restaurantId)
        {
            Orders? order = base.DbSet
                .Where(o => o.Id == orderId && o.RestaurantId == restaurantId)
                .FirstOrDefault();

            return order;
        }

        public List<Orders> GetOrdersByTable(int tableId, int restaurantId)
        {
            List<Orders> orders = base.DbSet
                .Where(o => o.TableId == tableId && o.RestaurantId == restaurantId)
                .ToList();

            return orders;
        }

        public List<Orders> GetOrdersByUserId(int userId, int restaurantId)
        {
            List<Orders> orders = base.DbSet
                .Where(o => o.UserId == userId && o.RestaurantId == restaurantId)
                .ToList();

            return orders;
        }

        public List<Orders> GetOrdersByRestaurantId(int restaurantId)
        {
            List<Orders>? orders = base.DbSet
                .Where(o => o.RestaurantId == restaurantId)
                .ToList();

            return orders;
        }

        public async Task UpdateAsync(int id, Orders updatedOrder)
        {
            _context.Orders.Update(updatedOrder);
            await _context.SaveChangesAsync();
        }
    }
}
