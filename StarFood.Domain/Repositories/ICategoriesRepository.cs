using StarFood.Domain.Entities;

namespace StarFood.Application.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<List<Categories>> GetAllAsync(int restaurantId);
        Task<Categories> GetByIdAsync(int id);
        Task CreateAsync(Categories category);
        Task UpdateAsync(int id, Categories category);
        Task ChangeAvailability(int id, bool isAvailable);
        Task DeleteAsync(int id);
    }
}
