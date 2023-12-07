using Microsoft.EntityFrameworkCore;
using StarFood.Application.Interfaces;
using StarFood.Domain.Base;
using StarFood.Domain.Entities;

namespace StarFood.Infrastructure.Data.Repositories
{
    public class ProductRepository : BaseRepository<Products>, IProductsRepository
    {

        public ProductRepository(StarFoodDbContext context) : base(context)
        {
        }

        public List<Products> GetProductsByRestaurantId(Restaurants restaurant)
        {
            List<Products>? products =  base.DbSet
                .Where(d => d.RestaurantId == restaurant.RestaurantId)
                .ToList();

            return products;
        }

        public Products GetProductById(Restaurants restaurant, int id)
        {
            Products? product = base.DbSet
                .Where(p => p.Id == id && p.RestaurantId == restaurant.RestaurantId)
                .FirstOrDefault();

            return product;
        }

        public List<Products> GetProductByCategoryId(Restaurants restaurant, int categoryId)
        {
            List<Products>? products = base.DbSet
                .Where(p => p.CategoryId == categoryId && p.RestaurantId == restaurant.RestaurantId)
                .ToList();

            return products;
        }
    }
}
