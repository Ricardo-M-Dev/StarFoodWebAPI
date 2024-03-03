using StarFood.Application.Base.Messages;
using System.Text.Json.Serialization;

namespace StarFood.Domain.Commands
{
    public class CreateRestaurantCommand : Command<ICommandResponse>
    {
        public string Name { get; set; }
    }

    public class UpdateRestaurantCommand : Command<ICommandResponse>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; }
        public bool Deleted { get; set; }
    }

    public class StatusRestaurantCommand : Command<ICommandResponse>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool Status { get; set; }
    }

    public class DeleteRestaurantCommand : Command<ICommandResponse>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}
