using Microsoft.EntityFrameworkCore;
using StarFood.Domain.Base;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class VariationsRepository : BaseRepository<Variations>, IVariationsRepository
    {
        private readonly StarFoodDbContext _context;

        public VariationsRepository(StarFoodDbContext context):base(context)
        {
            _context = context;

        }

        public List<Variations> GetVariationsByRestaurantId(Restaurants restaurant)
        {
            List<Variations> variations = base.DbSet
                .Where(v => v.RestaurantId == restaurant.RestaurantId)
                .ToList();

            return variations;
        }

        public Variations GetVariationById(Restaurants restaurant, int id)
        {
            Variations? variation = base.DbSet
                .Where(v => v.Id == id && v.RestaurantId == restaurant.RestaurantId)
                .FirstOrDefault();

            return variation;
        }

        public List<Variations> GetVariationsByProductId(Restaurants restaurant, int productId)
        {
            List<Variations> variations = base.DbSet
                .Where(v => v.ProductId == productId && v.IsAvailable == true && v.RestaurantId == restaurant.RestaurantId)
                .ToList();

            return variations;
        }
    }
}
