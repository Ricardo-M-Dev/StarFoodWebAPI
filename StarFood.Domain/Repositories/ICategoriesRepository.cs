using StarFood.Application.Base;
using StarFood.Domain.Entities;

namespace StarFood.Application.Interfaces
{
    public interface ICategoriesRepository : IBaseRepository<Categories>
    {
        List<Categories> GetCategoriesByRestaurantId(Restaurants restaurant);
        Categories GetCategoryById(Restaurants restaurant, int id);
    }
}
