using StarFood.Domain.Entities;

namespace StarFood.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Products>> GetAllAsync(int restaurantId);
        Task<Products> GetByIdAsync(int id);
        Task<List<Products>> GetByType(int productTypeId);
        Task<List<Products>> GetByCategory(int categoryId);
        Task CreateAsync(Products product);
        Task UpdateAsync(int id, Products updatedProduct);
        Task ChangeAvailability(int id, bool isAvailable);
        Task DeleteAsync(int id);
    }
}
