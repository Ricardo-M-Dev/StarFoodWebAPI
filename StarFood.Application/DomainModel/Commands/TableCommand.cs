using StarFood.Application.Base.Messages;
using System.Text.Json.Serialization;

namespace StarFood.Application.DomainModel.Commands
{
    public class CreateTableCommand : Command<ICommandResponse>
    {
        public int Number { get; set; }
        public string? Barcode { get; set; }
    }

    public class UpdateTableCommand : Command<ICommandResponse> 
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int Number { get; set; }
        public string? Barcode { get; set; }
    }

    public class StatusTableCommand : Command<ICommandResponse>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool Status { get; set; }
    }

    public class DeleteTableCommand : Command<ICommandResponse>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}
