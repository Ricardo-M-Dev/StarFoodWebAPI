using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Application.CommandHandlers
{
    public class UpdateProductVariationCommandHandler : ICommandHandler<UpdateProductVariationCommand, ProductVariations>
    {
        private readonly IProductVariationsRepository _productVariationsRepository;

        public UpdateProductVariationCommandHandler(IProductVariationsRepository productVariationsRepository)
        {
            _productVariationsRepository = productVariationsRepository;
        }

        public async Task<ProductVariations> HandleAsync(UpdateProductVariationCommand command)
        {
            var updatedProductVariation = new ProductVariations
            {
                ProductId = command.ProductId,
                VariationId = command.VariationId,
            };

            await _productVariationsRepository.UpdateAsync(command.Id, updatedProductVariation);
            return updatedProductVariation;
        }
    }
}
