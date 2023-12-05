using StarFood.Application.Base;
using StarFood.Domain.Entities;

namespace StarFood.Application.Interfaces
{
    public interface IProductsRepository : IBaseRepository<Products>
    {
        List<Products> GetProductsByRestaurantId(Restaurants restaurant);
        Products GetProductById(Restaurants restaurant, int id);
        List<Products> GetProductByCategoryId(Restaurants restaurant, int categoryId);
    }
}
