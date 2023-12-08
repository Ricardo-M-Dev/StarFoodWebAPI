using StarFood.Domain.Entities;

namespace StarFood.Domain.Repositories
{
    public interface IOrdersRepository
    {
        Task<List<Orders>> GetAllAsync(int restaurantId);
        Task<Orders> GetByIdAsync(int id);
        Task<Orders> GetByTableAsync(int table);
        Task<Orders> GetByWaiterNameAsync(string waiterName);
        Task CreateAsync(Orders order);
        Task UpdateAsync(int id, Orders updatedOrder);
        Task DeleteAsync(int id);
    }
}
