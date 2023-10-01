using StarFood.Domain.Entities;

namespace StarFood.Domain.Repositories
{
    public interface IVariationsRepository
    {
        Task<List<Variations>> GetAllAsync(string restaurantId);
        Task<Variations> GetByIdAsync(int id);
        Task CreateAsync(Variations productVariations);
        Task UpdateAsync(int id, Variations productVariations);
        Task ChangeAvailability(int id, bool isAvailable);
        Task DeleteAsync(int id);
    }
}
