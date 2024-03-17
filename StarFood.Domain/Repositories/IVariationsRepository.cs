using StarFood.Application.Base;
using StarFood.Domain.Entities;

namespace StarFood.Domain.Repositories
{
    public interface IVariationsRepository : IBaseRepository<Variations>
    {
        List<Variations> GetVariationsByRestaurantId(int restaurantId);
        List<Variations> GetActiveVariationsByRestaurantId(int restaurantId);
        Variations GetVariationById(int id, int restaurantId);
        List<Variations> GetVariationsByProductId(int productId, int restaurantId);
    }
}
