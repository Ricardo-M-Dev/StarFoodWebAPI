using MediatR;
using StarFood.Domain.Entities;
using StarsFoodAPI.Services.HttpContext;

namespace StarFood.Application.Base.Messages
{
    public abstract class Command<T> : ICommand, IRequest<T>
    {

        public int RestaurantId { get; set; }
        public DateTime DateTime { get; }
        public Restaurants Restaurant { get; set; }

        public void UpdateRequestInfo(RequestState requestState, Restaurants restaurant)
        {
            Restaurant = restaurant;
            RestaurantId = requestState.RestaurantId;
        }
    }
}
