using StarFood.Application.Interfaces;
using StarFood.Domain;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Application.Handlers
{
    public class ProductOrderCommandHandler : ICommandHandler<CreateProductOrder, ProductOrder>
    {
        private readonly IProductOrderRepository _productOrderRepository;

        public ProductOrderCommandHandler(IProductOrderRepository productOrderRepository)
        {
            _productOrderRepository = productOrderRepository;
        }

        public Task<ProductOrder> HandleAsync(CreateProductOrder command, int restaurantId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductOrder>> HandleAsyncList(List<CreateProductOrder> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
