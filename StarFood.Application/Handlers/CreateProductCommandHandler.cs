using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Application.CommandHandlers
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Products>
    {
        private readonly IProductsRepository _productRepository;
        private readonly IVariationsRepository _variationsRepository;

        public CreateProductCommandHandler(IProductsRepository productRepository, IVariationsRepository variationsRepository)
        {
            _productRepository = productRepository;
            _variationsRepository = variationsRepository;
        }

        public async Task<Products> HandleAsync(CreateProductCommand newProductCommand, int restaurantId)
        {
            var newProduct = new Products
            {
                Name = newProductCommand.Name,
                Description = newProductCommand.Description,
                ImgUrl = newProductCommand.ImgUrl,
                CreatedTime = DateTime.Now,
                CategoryId = newProductCommand.CategoryId,
                RestaurantId = restaurantId,
            };

            await _productRepository.CreateAsync(newProduct);


            foreach (var variation in newProductCommand.Variations)
            {
                var newVariation = new Variations
                {
                    Description = variation.Description,
                    ProductId = newProduct.Id,
                    Value = variation.Value,
                    CreatedTime = DateTime.Now,
                    RestaurantId= restaurantId,
                };

                await _variationsRepository.CreateAsync(newVariation);

            };

            return newProduct;
        }

        public Task<List<Products>> HandleAsyncList(List<CreateProductCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
