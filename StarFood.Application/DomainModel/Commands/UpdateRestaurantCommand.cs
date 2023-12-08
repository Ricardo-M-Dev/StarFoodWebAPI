
using StarFood.Application.Base.Messages;

namespace StarFood.Domain.Commands
{
    public class UpdateRestaurantCommand : Command<ICommandResponse>
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string? Name { get; set; }
        public bool IsAvailable { get; set; }
    }
}
