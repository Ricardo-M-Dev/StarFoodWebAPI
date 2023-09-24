using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Application.CommandHandlers
{
    public class CreateDishCommandHandler : ICommandHandler<CreateDishCommand, Dishes>
    {
        private readonly IDishesRepository _dishRepository;
        private readonly IProductTypesRepository _productTypeRepository;
        private readonly ICategoriesRepository _categoryRepository;

        public CreateDishCommandHandler(IDishesRepository dishRepository, IProductTypesRepository productTypeRepository, ICategoriesRepository categoryRepository)
        {
            _dishRepository = dishRepository;
            _productTypeRepository = productTypeRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Dishes> HandleAsync(CreateDishCommand command)
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

            var newDish = new Dishes
            {
                Name = command.Name,
                Description = command.Description,
                ProductTypeId = command.ProductTypeId,
                CategoryId = command.CategoryId,
                RestaurantId = command.RestaurantId,
            };

            await _dishRepository.CreateAsync(newDish);
            return newDish;
        }
    }
}
