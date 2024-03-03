using StarFood.Application.Base;
using StarFood.Domain.Entities;

namespace StarFood.Domain.Repositories
{
    public interface IProductOrderRepository : IBaseRepository<ProductOrder>
    {
        ProductOrder GetProductOrderById(int id, int restaurantId);
        List<ProductOrder> GetProductsOrderByOrderId(int orderId, int restaurantId);
    }
}
