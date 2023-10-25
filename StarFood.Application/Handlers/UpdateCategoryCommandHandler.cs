using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;

namespace StarFood.Application.CommandHandlers
{
    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, Categories>
    {
        private readonly ICategoriesRepository _categoryRepository;

        public UpdateCategoryCommandHandler(ICategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Categories> HandleAsync(UpdateCategoryCommand command, int restaurantId)
        {
            if (string.IsNullOrEmpty(command.CategoryName))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }
            Categories updatedCategory = new Categories
            {
                CategoryName = command.CategoryName,
                UpdateTime = DateTime.Now,
                RestaurantId = restaurantId,
                IsAvailable = command.IsAvailable,
            };

            updatedCategory.SetId(command.Id);

            await _categoryRepository.UpdateAsync(updatedCategory);
            return updatedCategory;
        }

        public Task<List<Categories>> HandleAsyncList(List<UpdateCategoryCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
