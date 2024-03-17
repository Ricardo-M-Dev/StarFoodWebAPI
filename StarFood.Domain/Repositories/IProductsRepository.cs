using StarFood.Application.Base;
using StarFood.Domain.Entities;

namespace StarFood.Application.Interfaces
{
    public interface IProductsRepository : IBaseRepository<Products>
    {
        List<Products> GetProductsByRestaurantId(int restaurantId);
        List<Products> GetActiveProductsByRestaurantId(int restaurantId);
        Products GetProductById(int id, int restaurantId);
        List<Products> GetProductByCategoryId(int categoryId, int restaurantId);
        Products GetProductByVariation (int productId, int restaurantId);
    }
}
