using MediatR;
using StarFood.Application.Base;
using StarFood.Application.Base.Messages;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;

namespace StarFood.Application.Handlers
{
    public class RestaurantsCommandHandler :
        IRequestHandler<CreateRestaurantCommand, ICommandResponse>,
        IRequestHandler<UpdateRestaurantCommand, ICommandResponse>
    {
        private readonly IRestaurantsRepository _restaurantRepository;
        private readonly IUnitOfWork<StarFoodDbContext> _unitOfWork;

        public RestaurantsCommandHandler(
            IRestaurantsRepository restaurantRepository,
            IUnitOfWork<StarFoodDbContext> unitOfWork
        )
        {
            _restaurantRepository = restaurantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResponse> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            Restaurants? newRestaurant = new Restaurants
            {
                RestaurantId = request.RestaurantId,
                Name = request.Name,
            };

            _restaurantRepository.Add(newRestaurant);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessCommandResponse(newRestaurant);
        }

        public async Task<ICommandResponse> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var updateRestaurant = _restaurantRepository.GetByRestaurantId(request.RestaurantId);

            if (updateRestaurant == null)
            {
                return new ErrorCommandResponse();
            }

            updateRestaurant.Name = request.Name;
            updateRestaurant.RestaurantId = request.RestaurantId;
            updateRestaurant.IsAvailable = request.IsAvailable;

            _restaurantRepository.Edit(updateRestaurant);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessCommandResponse(updateRestaurant);
        }
    }
}
