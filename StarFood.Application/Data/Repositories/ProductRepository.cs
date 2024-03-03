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

        /// <summary>
        /// Busca por Restaurante.
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns>Produtos cadastrados no Restaurante.</returns>
        public List<Products> GetProductsByRestaurantId(int restaurantId)
        {
            List<Products>? products =  base.DbSet
                .Where(p => p.RestaurantId == restaurantId)
                .ToList();

            return products;
        }

        /// <summary>
        /// Busca por Restaurante.
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns>Produtos <b>ativos</b> cadastrados no Restaurante.</returns>
        public List<Products> GetActiveProductsByRestaurantId(int restaurantId)
        {
            List<Products>? products = base.DbSet
                .Where(p => p.RestaurantId == restaurantId && p.Status == true)
                .ToList();

            return products;
        }

        /// <summary>
        /// Busca por Id do Produto.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="restaurantId"></param>
        /// <returns>Produto pelo Id.</returns>
        public Products GetProductById(int id, int restaurantId)
        {
            Products? product = base.DbSet
                .Where(p => p.Id == id && p.RestaurantId == restaurantId)
                .FirstOrDefault();

            return product;
        }

        /// <summary>
        /// Busca pelo Id da Categoria
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="restaurantId"></param>
        /// <returns>Produtos cadastrados cadastrados na Categoria.</returns>
        public List<Products> GetProductByCategoryId(int categoryId, int restaurantId)
        {
            List<Products>? products = base.DbSet
                .Where(p => p.CategoryId == categoryId && p.RestaurantId == restaurantId)
                .ToList();

            return products;
        }

        /// <summary>
        /// Busca pelo Id do Produto na Variação.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="restaurantId"></param>
        /// <returns>Produto que a Variação pertence.</returns>
        public Products GetProductByVariation(int productId, int restaurantId)
        {
            Products? product = base.DbSet
                .Where(p => p.Id == productId && p.RestaurantId == restaurantId)
                .FirstOrDefault();

            return product;
        }
    }
}
