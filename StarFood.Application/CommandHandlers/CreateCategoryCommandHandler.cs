using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;

namespace StarFood.Application.CommandHandlers
{
    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Categories>
    {
        private readonly ICategoriesRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Categories> HandleAsync(CreateCategoryCommand command)
        {
            if (string.IsNullOrEmpty(command.CategoryName))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }

            var newCategory = new Categories
            {
                CategoryName = command.CategoryName,
                RestaurantId = command.RestaurantId,
            };

            await _categoryRepository.CreateAsync(newCategory);
            return newCategory;
        }
    }
}
