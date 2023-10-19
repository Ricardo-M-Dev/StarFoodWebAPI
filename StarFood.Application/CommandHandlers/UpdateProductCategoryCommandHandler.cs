using Microsoft.EntityFrameworkCore;
using StarFood.Application.Interfaces;
using StarFood.Application.Models;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Infrastructure.Data;
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

        public async Task<ProductCategories> HandleAsync(UpdateProductCategoryCommand command, int restaurantId)
        {
            if (string.IsNullOrEmpty(command.CategoryName))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.");
            }
            ProductCategories updatedCategory = new ProductCategories
            {
                CategoryName = command.CategoryName,
                RestaurantId = restaurantId,
                IsAvailable = command.IsAvailable,
            };

            updatedCategory.SetId(command.Id);

            await _categoryRepository.UpdateAsync(updatedCategory);
            return updatedCategory;
        }

        public Task<List<ProductCategories>> HandleAsyncList(List<UpdateProductCategoryCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
