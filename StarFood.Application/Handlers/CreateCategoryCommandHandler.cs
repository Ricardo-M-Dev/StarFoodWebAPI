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

        public async Task<Categories> HandleAsync(CreateCategoryCommand command, int restaurantId)
        {
            if (string.IsNullOrEmpty(command.CategoryName))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }

            var newCategory = new Categories
            {
                CreatedTime = DateTime.Now,
                CategoryName = command.CategoryName,
                ImgUrl = command.ImgUrl,
                RestaurantId = restaurantId,
            };

            await _categoryRepository.CreateAsync(newCategory);
            return newCategory;
        }

        public Task<List<Categories>> HandleAsyncList(List<CreateCategoryCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
