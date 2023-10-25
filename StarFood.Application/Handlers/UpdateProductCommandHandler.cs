using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Infrastructure.Data;

namespace StarFood.Application.CommandHandlers
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Products>
    {
        private readonly StarFoodDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICategoriesRepository _categoryRepository;

        public UpdateProductCommandHandler(StarFoodDbContext context, IProductRepository productRepository, ICategoriesRepository categoryRepository)
        {
            _context = context;
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

            var product = _context.Products.FindAsync(command.Id);

            if (product.Result == null)
            {
                return product.Result;
            }
            else
            {
                product.Result.Name = command.Name;
                product.Result.Description = command.Description;
                product.Result.CategoryId = command.CategoryId;
                product.Result.ImgUrl = command.ImgUrl;
                product.Result.UpdateTime = DateTime.Now;
                product.Result.IsAvailable = command.IsAvailable;

                await _productRepository.UpdateAsync(command.Id, product.Result);

                return product.Result;
            }
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
