using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Application.CommandHandlers
{
    public class CreateRestaurantCommandHandler : ICommandHandler<CreateRestaurantCommand, Restaurants>
    {
        private readonly IRestaurantsRepository _createRestaurantRepository;

        public CreateRestaurantCommandHandler(IRestaurantsRepository createRestaurantRepository)
        {
            _createRestaurantRepository = createRestaurantRepository;
        }

        public async Task<Restaurants> HandleAsync(CreateRestaurantCommand restaurant, int restaurantId)
        {
            var newRestaurant = new Restaurants();
            if (string.IsNullOrEmpty(restaurant.Name))
            {
                throw new ArgumentException("O nome da variação é obrigatório.");
            }

            newRestaurant = new Restaurants
            {
                Name = restaurant.Name,
                RestaurantId = restaurantId
            };

            await _createRestaurantRepository.CreateAsync(newRestaurant);

            return newRestaurant;
        }

        public Task<List<Restaurants>> HandleAsyncList(List<CreateRestaurantCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
