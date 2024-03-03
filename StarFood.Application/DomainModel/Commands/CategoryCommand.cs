using StarFood.Application.Base.Messages;
using System.Text.Json.Serialization;

namespace StarFood.Domain.Commands
{
    public class CreateCategoryCommand : Command<ICommandResponse>
    {
        public string Name { get; set; }
    }

    public class UpdateCategoryCommand : Command<ICommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StatusCategoryCommand : Command<ICommandResponse>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool Status { get; set; }
    }

    public class DeleteCategoryCommand : Command<ICommandResponse>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}
