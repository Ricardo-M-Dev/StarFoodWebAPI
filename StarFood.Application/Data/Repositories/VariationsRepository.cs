using Microsoft.EntityFrameworkCore;
using StarFood.Domain.Base;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class VariationsRepository : BaseRepository<Variations>, IVariationsRepository
    {

        public VariationsRepository(StarFoodDbContext context):base(context)
        {
        }

        public List<Variations> GetVariationsByRestaurantId(int restaurantId)
        {
            List<Variations> variations = base.DbSet
                .Where(v => v.RestaurantId == restaurantId)
                .ToList();

            return variations;
        }

        public List<Variations> GetActiveVariationsByRestaurantId(int restaurantId)
        {
            List<Variations> variations = base.DbSet
                .Where(v => v.RestaurantId == restaurantId && v.Status == true)
                .ToList();

            return variations;
        }

        public Variations GetVariationById(int id, int restaurantId)
        {
            Variations? variation = base.DbSet
                .Where(v => v.Id == id && v.RestaurantId == restaurantId)
                .FirstOrDefault();

            return variation;
        }

        public List<Variations> GetVariationsByProductId(int productId, int restaurantId)
        {
            List<Variations> variations = base.DbSet
                .Where(v => v.ProductId == productId && v.Status == true && v.RestaurantId == restaurantId)
                .ToList();

            return variations;
        }
    }
}
