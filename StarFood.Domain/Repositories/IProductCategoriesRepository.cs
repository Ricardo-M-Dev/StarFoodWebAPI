using StarFood.Domain.Entities;

namespace StarFood.Application.Interfaces
{
    public interface IProductCategoriesRepository
    {
        Task<List<ProductCategories>> GetAllAsync(int restaurantId);
        Task<ProductCategories> GetByIdAsync(int id);
        Task CreateAsync(ProductCategories category);
        Task UpdateAsync(int id, ProductCategories category);
        Task ChangeAvailability(int id, bool isAvailable);
        Task DeleteAsync(int id);
    }
}
