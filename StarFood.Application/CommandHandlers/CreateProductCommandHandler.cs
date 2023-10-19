using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Application.CommandHandlers
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Products>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoriesRepository _categoryRepository;

        public CreateProductCommandHandler(IProductRepository productRepository, IProductCategoriesRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Products> HandleAsync(CreateProductCommand command, int restaurantId)
        {
            if (string.IsNullOrEmpty(command.Name))
            {
                throw new ArgumentException("O nome do prato é obrigatório.");
            }

            if (string.IsNullOrEmpty(command.Description))
            {
                throw new ArgumentException("A descrição do prato é obrigatória.");
            }

            if (command.ProductTypeId == 0)
            {
                throw new ArgumentException("O tipo de produto é obrigatório.");
            }

            if (command.CategoryId == 0)
            {
                throw new ArgumentException("A categoria é obrigatória.");
            }

            if (_categoryRepository.GetByIdAsync(command.CategoryId, restaurantId) == null)
            {
                throw new ArgumentException("Categoria não encontrada.");
            }

            var newProduct = new Products
            {
                Name = command.Name,
                Description = command.Description,
                CategoryId = command.CategoryId,
                RestaurantId = command.RestaurantId,
            };

            await _productRepository.CreateAsync(newProduct);

            return newProduct;
        }
        public Task<Products> HandleAsyncList(List<CreateProductCommand> commandList)
        {
            throw new NotImplementedException();
        }

        public Task<List<Products>> HandleAsyncList(List<CreateProductCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
