using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Application.CommandHandlers
{
    public class CreateProductVariationCommandHandler : ICommandHandler<CreateProductVariationCommand, ProductVariations>
    {
        private readonly IProductVariationsRepository _productVariationsRepository;

        public CreateProductVariationCommandHandler(IProductVariationsRepository productVariationsRepository)
        {
            _productVariationsRepository = productVariationsRepository;
        }

        public async Task<ProductVariations> HandleAsync(CreateProductVariationCommand command)
        {
            var newProductVariation = new ProductVariations
            {
                ProductId = command.ProductId,
                VariationId = command.VariationId,
                RestaurantId = command.RestaurantId,
            };

            await _productVariationsRepository.CreateAsync(newProductVariation);
            return newProductVariation;
        }
    }
}
