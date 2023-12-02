using StarFood.Application.Interfaces;
using StarFood.Domain;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StarFood.Application.Handlers
{
    public class CreateOrderProductsCommandHandler : ICommandHandler<CreateOrderProductsCommand, OrderProducts>
    {
        private readonly IOrderProductsRepository _orderProductsRepository;

        public CreateOrderProductsCommandHandler(IOrderProductsRepository orderProductsRepository)
        {
            _orderProductsRepository = orderProductsRepository;
        }

        public Task<OrderProducts> HandleAsync(CreateOrderProductsCommand command, int restaurantId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderProducts>> HandleAsyncList(List<CreateOrderProductsCommand> commandList, int restaurantId)
        {
            List<OrderProducts>? newOrderProductsList = new();

            foreach (var orderProduct in commandList)
            {
                var newOrderProduct = new OrderProducts
                {
                    OrderId = orderProduct.OrderId,
                    ProductId = orderProduct.ProductId,
                    VariationId = orderProduct.VariationId,
                    Quantity = orderProduct.Quantity,
                    Status = OrderStatus.Waiting,
                };

                await _orderProductsRepository.CreateAsync(newOrderProduct);
                newOrderProductsList.Add(newOrderProduct);
            }

            return newOrderProductsList;
        }
    }
}
