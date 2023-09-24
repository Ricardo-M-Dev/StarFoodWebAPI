using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Application.CommandHandlers
{
    public class CreateProductTypeCommandHandler : ICommandHandler<CreateProductTypeCommand, ProductTypes>
    {
        private readonly IProductTypesRepository _productTypeRepository;

        public CreateProductTypeCommandHandler(IProductTypesRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        public async Task<ProductTypes> HandleAsync(CreateProductTypeCommand command)
        {
            if (string.IsNullOrEmpty(command.TypeName))
            {
                throw new ArgumentException("O nome do tipo de produto é obrigatório");
            }

            var newProductType = new ProductTypes
            {
                TypeName = command.TypeName,
                RestaurantId = command.RestaurantId,
            };

            await _productTypeRepository.CreateAsync(newProductType);
            return newProductType;
        }
    }
}
