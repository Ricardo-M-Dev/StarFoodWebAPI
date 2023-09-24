using StarFood.Domain.Entities;

namespace StarFood.Application.Interfaces
{
    public interface IDishesRepository
    {
        Task<List<Dishes>> GetAllAsync(int restaurantId);
        Task<Dishes> GetByIdAsync(int id);
        Task<List<Dishes>> GetByType(int productTypeId);
        Task<List<Dishes>> GetByCategory(int categoryId);
        Task CreateAsync(Dishes dish);
        Task UpdateAsync(int id, Dishes updatedDish);
        Task ChangeAvailability(int id, bool isAvailable);
        Task DeleteAsync(int id);
    }
}
