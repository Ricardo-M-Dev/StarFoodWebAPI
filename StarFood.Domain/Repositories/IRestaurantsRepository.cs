using StarFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarFood.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<List<Restaurants>> GetAllAsync();
        Task<Restaurants> GetByIdAsync(int id);
        Task CreateAsync(Restaurants restaurant);
        Task UpdateAsync(int id, Restaurants updatedRestaurant);
        Task DeleteAsync(int id);
    }
}
