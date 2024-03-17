using StarFood.Domain.Base;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class ProductsOrderRepository : BaseRepository<ProductOrder>, IProductOrderRepository
    {
        private readonly StarFoodDbContext _context;

        public ProductsOrderRepository(StarFoodDbContext context): base(context)
        {
            _context = context;
        }

        public ProductOrder GetProductOrderById(int id, int restaurantId)
        {
            throw new NotImplementedException();
        }

        public List<ProductOrder> GetProductsOrderByOrderId(int orderId, int restaurantId)
        {
            List<ProductOrder> productsOrder = base.DbSet
                .Where(po => po.OrderId == orderId)
                .ToList();

            return productsOrder;
        }
    }
}
