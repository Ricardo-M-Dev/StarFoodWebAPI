using StarFood.Application.Base;
using StarFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarFood.Domain.Repositories
{
    public interface IRestaurantsRepository : IBaseRepository<Restaurants>
    {
        void SetStatus(int restaurantId, bool status);
        Restaurants GetByRestaurantId(int restaurantId);
    }
}
