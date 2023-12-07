using StarFood.Application.Base.Messages;

namespace StarFood.Domain.Commands
{
    public class CreateRestaurantCommand : Command<ICommandResponse>
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
