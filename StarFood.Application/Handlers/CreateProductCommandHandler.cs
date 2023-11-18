using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;

namespace StarFood.Application.CommandHandlers
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Products>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoriesRepository _categoryRepository;

        public CreateProductCommandHandler(IProductRepository productRepository, ICategoriesRepository categoryRepository)
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
                ImgUrl = command.ImgUrl,
                CreatedTime = DateTime.Now,
                CategoryId = command.CategoryId,
                RestaurantId = restaurantId,
            };

            await _productRepository.CreateAsync(newProduct);

            return newProduct;
        }

        public Task<List<Products>> HandleAsyncList(List<CreateProductCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
