using MediatR;
using StarFood.Domain.Entities;
using StarsFoodAPI.Services.HttpContext;

namespace StarFood.Application.Base.Messages
{
    public abstract class Command<T> : ICommand, IRequest<T>
    {
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int RestaurantId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public DateTime DateTime { get; }
        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Restaurants? Restaurant { get; set; }

        public void UpdateRequestInfo(RequestState requestState, Restaurants restaurant)
        {
            Restaurant = restaurant;
            RestaurantId = requestState.RestaurantId;
        }
    }
}
