using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarFood.Application.CommandHandlers
{
    public class UpdateProductTypeCommandHandler : ICommandHandler<UpdateProductTypeCommand, ProductTypes>
    {
        private readonly IProductTypesRepository _productTypeRrepository;

        public UpdateProductTypeCommandHandler(IProductTypesRepository productTypeRrepository)
        {
            _productTypeRrepository = productTypeRrepository;
        }

        public async Task<ProductTypes> HandleAsync(UpdateProductTypeCommand command)
        {
            if (string.IsNullOrEmpty(command.TypeName))
            {
                throw new ArgumentException("O nome do tipo é obrigatório.");
            }

            var updateProductType = new ProductTypes
            {
                TypeName = command.TypeName
            };

            await _productTypeRrepository.UpdateAsync(updateProductType);
            return updateProductType;
        }
    }
}
