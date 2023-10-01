using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;

namespace StarFood.Application.CommandHandlers
{
    public class CreateProductCategoryCommandHandler : ICommandHandler<CreateProductCategoryCommand, ProductCategories>
    {
        private readonly IProductCategoriesRepository _categoryRepository;

        public CreateProductCategoryCommandHandler(IProductCategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ProductCategories> HandleAsync(CreateProductCategoryCommand command)
        {
            if (string.IsNullOrEmpty(command.CategoryName))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }

            var newCategory = new ProductCategories
            {
                CategoryName = command.CategoryName,
                RestaurantId = command.RestaurantId,
            };

            await _categoryRepository.CreateAsync(newCategory);
            return newCategory;
        }

        public Task<List<ProductCategories>> HandleAsyncList(List<CreateProductCategoryCommand> commandList)
        {
            throw new NotImplementedException();
        }
    }
}
