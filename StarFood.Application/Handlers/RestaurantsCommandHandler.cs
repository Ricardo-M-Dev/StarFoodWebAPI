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
        IRequestHandler<UpdateRestaurantCommand, ICommandResponse>,
        IRequestHandler<StatusRestaurantCommand, ICommandResponse>,
        IRequestHandler<DeleteRestaurantCommand, ICommandResponse>
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
            try
            {
                Restaurants? newRestaurant = new Restaurants
                {
                    Name = request.Name,
                    RestaurantId = request.RestaurantId,
                    CreatedDate = DateTime.Now
                };

                _restaurantRepository.Add(newRestaurant);

                await _unitOfWork.SaveChangesAsync();

                return new SuccessCommandResponse(newRestaurant.Id, newRestaurant);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }
        }

        public async Task<ICommandResponse> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updateRestaurant = _restaurantRepository.GetByRestaurantId(request.RestaurantId);

                if (updateRestaurant == null)
                {
                    return new ErrorCommandResponse();
                }

                updateRestaurant.Name = request.Name;
                updateRestaurant.RestaurantId = request.RestaurantId;
                updateRestaurant.Status = request.Status;
                updateRestaurant.Deleted = request.Deleted;

                _restaurantRepository.Edit(updateRestaurant);
                await _unitOfWork.SaveChangesAsync();

                return new SuccessCommandResponse(updateRestaurant);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }
        }

        public async Task<ICommandResponse> Handle(StatusRestaurantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var statusRestaurant = _restaurantRepository.GetByRestaurantId(request.RestaurantId);

                if (statusRestaurant == null)
                {
                    return new ErrorCommandResponse();
                }

                statusRestaurant.Status = request.Status;

                _restaurantRepository.Edit(statusRestaurant);
                await _unitOfWork.SaveChangesAsync();

                return new SuccessCommandResponse(statusRestaurant);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }
        }

        public async Task<ICommandResponse> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleteRestaurant = _restaurantRepository.GetByRestaurantId(request.RestaurantId);

                if (deleteRestaurant == null)
                {
                    return new ErrorCommandResponse();
                }

                deleteRestaurant.Deleted = request.Deleted;

                _restaurantRepository.Edit(deleteRestaurant);
                await _unitOfWork.SaveChangesAsync();

                return new SuccessCommandResponse(deleteRestaurant);
            }
            catch (Exception ex)
            {
                return new ErrorCommandResponse(ex);
            }
        }
    }
}
