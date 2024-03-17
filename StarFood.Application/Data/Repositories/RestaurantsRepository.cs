using StarFood.Domain.Base;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class RestaurantsRepository : BaseRepository<Restaurants>, IRestaurantsRepository
    {
        public RestaurantsRepository(StarFoodDbContext context):base (context)
        { 

        }

        public Restaurants GetByRestaurantId(int restaurantId)
        {
            Restaurants? restaurant = base.DbSet
                .Where(r => r.RestaurantId == restaurantId)
                .FirstOrDefault();

            return restaurant;
        }
    }
}
