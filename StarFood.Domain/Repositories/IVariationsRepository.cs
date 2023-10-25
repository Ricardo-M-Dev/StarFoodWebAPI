using StarFood.Domain.Entities;

namespace StarFood.Domain.Repositories
{
    public interface IVariationsRepository
    {
        Task<List<Variations>> GetAllAsync(int restaurantId);
        Task<Variations> GetByIdAsync(int id);
        Task<List<Variations>> GetByProductIdAsync(int productId);
        Task CreateAsync(Variations productVariations);
        Task UpdateAsync(int id, Variations productVariations);
        Task DeleteAsync(int id);
    }
}
