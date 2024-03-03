using StarFood.Application.Base;
using StarFood.Domain.Entities;

namespace StarFood.Domain.Repositories
{
    public interface IRestaurantsRepository : IBaseRepository<Restaurants>
    {
        Restaurants GetByRestaurantId(int restaurantId);
    }
}
