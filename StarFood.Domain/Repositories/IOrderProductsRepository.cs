using StarFood.Domain.Entities;

namespace StarFood.Domain.Repositories
{
    public interface IOrderProductsRepository
    {
        Task CreateAsync(OrderProducts orderProducts);
    }
}
