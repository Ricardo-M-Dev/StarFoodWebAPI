using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StarFood.Domain;
using StarFood.Infrastructure.Auth;
using StarFood.Infrastructure.Data;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

namespace StarsFoodAPI.Services.HttpContext
{
    public class RequestState
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly DbContextOptions<StarFoodDbContext> _dbContextOptions;

        public RequestState(IHttpContextAccessor accessor, DbContextOptions<StarFoodDbContext> dbContextOptions)
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
                    string jwt = _accessor.HttpContext.Request.Headers["Authorization"].ToString();

                    string token = jwt.Split(" ")[1];

                    string restaurantId = DecodeToken(token, "restaurantId");

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

        public int NRestaurantId
        {
            get
            {
                using (var context = new StarFoodDbContext(_dbContextOptions))
                {
                    string jwt = _accessor.HttpContext.Request.Headers["Authorization"].ToString();

                    string token = jwt.Split(" ")[1];

                    string restaurantId = DecodeToken(token, "restaurantId");

                    return Convert.ToInt32(restaurantId);
                }
            }
        }

        public int UserId
        {
            get
            {
                string jwt = _accessor.HttpContext.Request.Headers["Authorization"].ToString();

                string token = jwt.Split(" ")[1];

                string userId = DecodeToken(token, "userId");

                return Convert.ToInt32(userId);
            }
        }

        public string DecodeToken(string token, string type)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            JwtSecurityToken decode = handler.ReadJwtToken(token);

            return decode.Claims.First(claim => claim.Type == type).Value;
        }
    }
}
