using StarFood.Domain.Entities;

namespace StarFood.Application.Interfaces
{
    public interface IProductsRepository
    {
        Task<List<Products>> GetAllAsync(int restaurantId);
        Task<Products> GetByIdAsync(int id);
        Task<List<Products>> GetByCategoryIdAsync(int categoryId);
        Task CreateAsync(Products product);
        Task UpdateAsync(int id, Products updatedProduct);
        Task DeleteAsync(int id);
    }
}
