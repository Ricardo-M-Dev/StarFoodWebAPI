using StarFood.Application.Interfaces;
using StarFood.Domain.Base;
using StarFood.Domain.Entities;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class CategoriesRepository : BaseRepository<Categories>, ICategoriesRepository
    {

        public CategoriesRepository(StarFoodDbContext context):base (context)
        {
        }

        public List<Categories> GetCategoriesByRestaurantId(int restaurantId)
        {
            List<Categories> categories = base.DbSet
                .Where(c => c.RestaurantId == restaurantId)
                .ToList();

            return categories;
        }

        public List<Categories> GetActiveCategoriesByRestaurantId(int restaurantId)
        {
            List<Categories> categories = base.DbSet
                .Where(c => c.RestaurantId == restaurantId && c.Status == true)
                .ToList();

            return categories;
        }

        public Categories GetCategoryById(int categoryId, int restaurantId)
        {
            Categories? getCategory = base.DbSet
                .Where(c => c.Id == categoryId && c.RestaurantId == restaurantId)
                .FirstOrDefault();

            return getCategory;
        }
    }
}
