using StarFood.Application.Base.Messages;

namespace StarFood.Application.DomainModel.Commands
{
    public class AuthCommand : Command<ICommandResponse>
    {
        public int UserId { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string Name { get; set; }
        public int RestaurantId { get; set; }
    }
}
