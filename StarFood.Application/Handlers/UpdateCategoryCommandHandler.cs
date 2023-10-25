using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Infrastructure.Data;

namespace StarFood.Application.CommandHandlers
{
    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, Categories>
    {
        private readonly StarFoodDbContext _context;
        private readonly ICategoriesRepository _categoryRepository;

        public UpdateCategoryCommandHandler(StarFoodDbContext context, ICategoriesRepository categoryRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }

        public async Task<Categories> HandleAsync(UpdateCategoryCommand command, int restaurantId)
        {
            if (string.IsNullOrEmpty(command.CategoryName))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }

            var category = await _context.Categories.FindAsync(command.Id);

            category.CategoryName = command.CategoryName;
            category.UpdateTime = DateTime.Now;
            category.ImgUrl = command.ImgUrl;
            category.IsAvailable = command.IsAvailable;

            await _categoryRepository.UpdateAsync(category);
            return category;
        }

        public Task<List<Categories>> HandleAsyncList(List<UpdateCategoryCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
