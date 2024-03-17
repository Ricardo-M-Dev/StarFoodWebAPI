using StarFood.Application.Base;
using StarFood.Domain.Entities;

namespace StarFood.Domain.Repositories
{
    public interface IOrdersRepository : IBaseRepository<Orders>
    {
        List<Orders> GetOrdersByRestaurantId(int restaurantId);
        Orders GetOrderById(int orderId, int restaurantId);
        List<Orders> GetOrdersByTable(int tableId, int restaurantId);
        List<Orders> GetOrdersByUserId(int userId, int restaurantId);
    }
}
