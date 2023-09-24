using StarFood.Domain.Entities;

namespace StarFood.Domain.Repositories
{
    public interface IProductVariationsRepository
    {
        Task<List<ProductVariations>> GetAllAsync(int restaurantId);
        Task<ProductVariations> GetByIdAsync(int id);
        Task CreateAsync(ProductVariations productVariations);
        Task UpdateAsync(int id, ProductVariations productVariations);
        Task ChangeAvailability(int id, bool isAvailable);
        Task DeleteAsync(int id);
    }
}
