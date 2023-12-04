using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;

namespace StarFood.Application.Handlers
{
    public class ProductCommandHandler : ICommandHandler<ProductCommand, Products>
    {
        private readonly StarFoodDbContext _context;
        private readonly IProductsRepository _productsRepository;
        private readonly IVariationsRepository _variationsRepository;


        public ProductCommandHandler(StarFoodDbContext context, IProductsRepository productsRepository, IVariationsRepository variationsRepository)
        {
            _context = context;
            _productsRepository = productsRepository;
            _variationsRepository = variationsRepository;
        }

        public Task<Products> HandleAsync(ProductCommand command, int restaurantId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Products>> HandleAsyncList(List<ProductCommand> commandList, int restaurantId)
        {
            List<Products>? products = await _productsRepository.GetAllAsync(restaurantId);

            if (products.Count == 0)
            {
                return new List<Products>();
            }
            else
            {
                foreach (var product in products)
                {
                    var category = await _context.Categories.FindAsync(product.CategoryId);
                    var variations = await _variationsRepository.GetByProductIdAsync(product.Id);

                    if (category != null)
                    {
                        product.Categories = category;
                    }

                    if (variations != null)
                    {
                        product.Variations = variations;
                    }
                }

                return products;
            }
        }
    }
}
