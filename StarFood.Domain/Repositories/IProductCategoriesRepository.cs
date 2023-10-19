using StarFood.Domain.Entities;

namespace StarFood.Application.Interfaces
{
    public interface IProductCategoriesRepository
    {
        Task<List<ProductCategories>> GetAllAsync(int restaurantId);
        Task<ProductCategories> GetByIdAsync(int id, int restaurantId);
        Task CreateAsync(ProductCategories category);
        Task UpdateAsync(ProductCategories category);
        Task DeleteAsync(int id, int restaurantId);
    }
}
