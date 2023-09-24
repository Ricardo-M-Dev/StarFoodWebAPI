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
    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, Categories>
    {
        private readonly ICategoriesRepository _categoryRepository;

        public UpdateCategoryCommandHandler(ICategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Categories> HandleAsync(UpdateCategoryCommand command)
        {
            if (string.IsNullOrEmpty(command.CategoryName))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }

            var updatedCategory = new Categories
            {
                CategoryName = command.CategoryName
            };

            await _categoryRepository.UpdateAsync(command.Id, updatedCategory);
            return updatedCategory;
        }
    }
}
