using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Application.CommandHandlers
{
    public class UpdateRestaurantCommandHandler : ICommandHandler<UpdateRestaurantCommand, Restaurants>
    {
        private readonly IRestaurantsRepository _restaurantRepository;

        public UpdateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository)
        {
            _restaurantRepository = restaurantsRepository;
        }

        public async Task<Restaurants> HandleAsync(UpdateRestaurantCommand command, int restaurantId)
        {
            var updateRestaurant = new Restaurants
            {
                Name = command.Name,
                RestaurantId = restaurantId,
                IsAvailable = command.IsAvailable
            };

            await _restaurantRepository.UpdateAsync(command.Id, updateRestaurant);

            return updateRestaurant;
        }

        public Task<List<Restaurants>> HandleAsyncList(List<UpdateRestaurantCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }

    
}
