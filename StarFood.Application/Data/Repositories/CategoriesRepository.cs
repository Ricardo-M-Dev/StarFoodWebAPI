using Microsoft.EntityFrameworkCore;
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

        public List<Categories> GetCategoriesByRestaurantId(Restaurants restaurant)
        {
            List<Categories> categories = base.DbSet
                .Where(c => c.RestaurantId == restaurant.RestaurantId)
                .ToList();

            return categories;
        }

        public Categories GetCategoryById(Restaurants restaurant, int id)
        {
            Categories? category = base.DbSet
                .Where(c => c.Id == id && c.RestaurantId == restaurant.RestaurantId)
                .FirstOrDefault();

            return category;
        }
    }
}
