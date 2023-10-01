using StarFood.Domain.Entities;

namespace StarFood.Domain.Repositories
{
    public interface IProductTypesRepository
    {
        Task<List<ProductTypes>> GetAllAsync(int restaurantId);
        Task<ProductTypes> GetByIdAsync(int id);
        Task CreateAsync(ProductTypes productTypes);
        Task UpdateAsync(int id, ProductTypes productTypes);
        Task ChangeAvailability(int id, bool isAvailable);
        Task DeleteAsync(int id);
    }
}
