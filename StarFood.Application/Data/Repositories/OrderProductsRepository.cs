using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class OrderProductsRepository : IOrderProductsRepository
    {
        private readonly StarFoodDbContext _context;

        public OrderProductsRepository(StarFoodDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(OrderProducts orderProducts)
        {
            await _context.AddAsync(orderProducts);
            await _context.SaveChangesAsync();
        }
    }
}
