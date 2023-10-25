using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;

namespace StarFood.Application.CommandHandlers
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Products>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoriesRepository _categoryRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository, ICategoriesRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Products> HandleAsync(UpdateProductCommand command, int restaurantId)
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

            var updatedProduct = new Products
            {
                Name = command.Name,
                Description = command.Description,
                CategoryId = command.CategoryId,
                UpdateTime = DateTime.Now,
                IsAvailable = command.IsAvailable,
            };

            await _productRepository.UpdateAsync(command.Id, updatedProduct);

            return updatedProduct;
        }

        public Task<List<Products>> HandleAsyncList(List<UpdateProductCommand> commandList)
        {
            throw new NotImplementedException();
        }

        public Task<List<Products>> HandleAsyncList(List<UpdateProductCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
