using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Application.CommandHandlers
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Products>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTypesRepository _productTypeRepository;
        private readonly IProductCategoriesRepository _categoryRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository, IProductTypesRepository productTypeRepository, IProductCategoriesRepository categoryRepository)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Products> HandleAsync(UpdateProductCommand command)
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

            if (_productTypeRepository.GetByIdAsync(command.ProductTypeId) == null)
            {
                throw new ArgumentException("Tipo de produto não encontrado.");
            }

            if (command.CategoryId == 0)
            {
                throw new ArgumentException("A categoria é obrigatória.");
            }

            if (_categoryRepository.GetByIdAsync(command.CategoryId) == null)
            {
                throw new ArgumentException("Categoria não encontrada.");
            }

            var updatedProduct = new Products
            {
                Name = command.Name,
                Description = command.Description,
                ProductTypeId = command.ProductTypeId,
                CategoryId = command.CategoryId,
            };

            await _productRepository.UpdateAsync(command.Id, updatedProduct);

            return updatedProduct;
        }

        public Task<List<Products>> HandleAsyncList(List<UpdateProductCommand> commandList)
        {
            throw new NotImplementedException();
        }
    }
}
