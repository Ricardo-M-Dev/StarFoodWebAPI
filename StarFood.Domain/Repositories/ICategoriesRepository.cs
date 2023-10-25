using StarFood.Domain.Entities;

namespace StarFood.Application.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<List<Categories>> GetAllAsync(int restaurantId);
        Task<Categories> GetByIdAsync(int id, int restaurantId);
        Task CreateAsync(Categories category);
        Task UpdateAsync(Categories category);
        Task DeleteAsync(int id, int restaurantId);
    }
}
