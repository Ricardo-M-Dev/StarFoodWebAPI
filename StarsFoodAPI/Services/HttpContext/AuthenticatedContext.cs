using StarFood.Infrastructure.Data;

namespace StarsFoodAPI.Services.HttpContext
{
    public class AuthenticatedContext
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly StarFoodDbContext _context;

        public AuthenticatedContext(IHttpContextAccessor accessor, StarFoodDbContext context)
        {
            _accessor = accessor;
            _context = context;
        }

        public int RestaurantId
        {
            get
            {
                     string? restaurantId = _accessor.HttpContext.Request.Headers["X-RestaurantId"].ToString();
                     var castInt = int.Parse(restaurantId);
                     return _context.Restaurants.FirstOrDefault(x => x.RestaurantId == castInt).RestaurantId;
            }
        }
    }
}
