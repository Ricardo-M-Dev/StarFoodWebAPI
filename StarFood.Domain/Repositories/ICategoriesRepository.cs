using StarFood.Application.Base;
using StarFood.Domain.Entities;

namespace StarFood.Application.Interfaces
{
    public interface ICategoriesRepository : IBaseRepository<Categories>
    {
        List<Categories> GetCategoriesByRestaurantId(int restaurantId);
        List<Categories> GetActiveCategoriesByRestaurantId(int restaurantId);
        Categories GetCategoryById(int categoryId, int restaurantId);
    }
}
