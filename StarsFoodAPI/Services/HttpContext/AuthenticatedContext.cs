using Microsoft.EntityFrameworkCore;
using StarFood.Infrastructure.Data;
using System.Data;

namespace StarsFoodAPI.Services.HttpContext
{
    public class AuthenticatedContext
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly DbContextOptions<StarFoodDbContext> _dbContextOptions;

        public AuthenticatedContext(IHttpContextAccessor accessor, DbContextOptions<StarFoodDbContext> dbContextOptions)
        {
            _accessor = accessor;
            _dbContextOptions = dbContextOptions;
        }

        public int RestaurantId
        {
            get
            {
                using (var context = new StarFoodDbContext(_dbContextOptions))
                {
                    string? restaurantId = _accessor.HttpContext.Request.Headers["X-RestaurantId"].ToString();

                    if (int.TryParse(restaurantId, out var castInt))
                    {
                        if (context.Database.GetDbConnection().State != ConnectionState.Open)
                        {
                            context.Database.GetDbConnection().Open();
                        }

                        var restaurant = context.Restaurants
                            .FirstOrDefault(x => x.RestaurantId == castInt);

                        return restaurant?.RestaurantId ?? 0;
                    }
                }

                return 0;
            }
        }
    }
}
