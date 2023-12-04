using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;
using StarFood.Infrastructure.Data.Repositories;

namespace StarFood.Application.CommandHandlers
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Products>
    {
        private readonly StarFoodDbContext _context;
        private readonly IProductsRepository _productRepository;
        private readonly ICategoriesRepository _categoryRepository;
        private readonly IVariationsRepository _variationsRepository;


        public UpdateProductCommandHandler(StarFoodDbContext context, IProductsRepository productRepository, ICategoriesRepository categoryRepository, IVariationsRepository variationsRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _variationsRepository = variationsRepository;

        }

        public async Task<Products> HandleAsync(UpdateProductCommand updateProductCommand, int restaurantId)
        {
            var updateProduct = await _context.Products.FindAsync(updateProductCommand.Id);

            if (updateProduct == null)
            {
                return new Products();
            }
            else
            {
                updateProduct.Name = updateProductCommand.Name;
                updateProduct.Description = updateProductCommand.Description;
                updateProduct.CategoryId = updateProductCommand.CategoryId;
                updateProduct.ImgUrl = updateProductCommand.ImgUrl;
                updateProduct.UpdateTime = DateTime.Now;
                updateProduct.IsAvailable = updateProductCommand.IsAvailable;

                await _productRepository.UpdateAsync(updateProductCommand.Id, updateProduct);

                List<Variations> updatedVariations = new List<Variations>();

                foreach (var variation in updateProductCommand.Variations)
                {
                    var updateVariation = await _context.Variations.FindAsync(variation.Id);

                    if (updateVariation != null)
                    {
                        updateVariation.Description = variation.Description;
                        updateVariation.ProductId = updateProduct.Id;
                        updateVariation.UpdateTime = DateTime.Now;
                        updateVariation.Value = variation.Value;
                        updateVariation.IsAvailable = variation.IsAvailable;

                        await _variationsRepository.UpdateAsync(updateVariation.Id, updateVariation);

                        updatedVariations.Add(updateVariation);
                    }
                    else
                    {
                        var newVariation = new Variations
                        {
                            Description = variation.Description,
                            ProductId = updateProduct.Id,
                            Value = variation.Value,
                            CreatedTime = DateTime.Now,
                            RestaurantId = restaurantId,
                        };

                        await _variationsRepository.UpdateAsync(newVariation.Id, newVariation);

                        updatedVariations.Add(newVariation);
                    }

                };
                return updateProduct;
            }
        }

        public Task<List<Products>> HandleAsyncList(List<UpdateProductCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
