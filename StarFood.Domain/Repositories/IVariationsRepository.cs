using StarFood.Application.Base;
using StarFood.Domain.Entities;

namespace StarFood.Domain.Repositories
{
    public interface IVariationsRepository : IBaseRepository<Variations>
    {
        List<Variations> GetVariationsByRestaurantId(Restaurants restaurant);
        Variations GetVariationById(Restaurants restaurant, int id);
        List<Variations> GetVariationsByProductId(Restaurants restaurant, int productId);
    }
}
