using StarFood.Application.Base.Messages;

namespace StarFood.Application.DomainModel.Commands
{
    public class StatusProductCommand : Command<ICommandResponse>
    {
        public StatusProductCommand() { }
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
    }
}
