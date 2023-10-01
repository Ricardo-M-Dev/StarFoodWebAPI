using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarFood.Application.CommandHandlers
{
    public class UpdateProductCategoryCommandHandler : ICommandHandler<UpdateProductCategoryCommand, ProductCategories>
    {
        private readonly IProductCategoriesRepository _categoryRepository;

        public UpdateProductCategoryCommandHandler(IProductCategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ProductCategories> HandleAsync(UpdateProductCategoryCommand command)
        {
            if (string.IsNullOrEmpty(command.CategoryName))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }

            var updatedCategory = new ProductCategories
            {
                CategoryName = command.CategoryName
            };

            await _categoryRepository.UpdateAsync(command.Id, updatedCategory);
            return updatedCategory;
        }
    }
}
