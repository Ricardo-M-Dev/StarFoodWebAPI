using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Application.Handlers
{
    public class OrderCommandHandler : ICommandHandler<CreateOrderCommand, Orders>
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrderCommandHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        public async Task<Orders> HandleAsync(CreateOrderCommand command, int restaurantId)
        {
            var newOrder = new Orders
            {
                Table = command.Table,
                OrderDate = DateTime.Now,
                Waiter = command.Waiter,
                RestaurantId = restaurantId
            };

            await _ordersRepository.CreateAsync(newOrder);

            return newOrder;
        }

        public Task<List<Orders>> HandleAsyncList(List<CreateOrderCommand> orderList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
