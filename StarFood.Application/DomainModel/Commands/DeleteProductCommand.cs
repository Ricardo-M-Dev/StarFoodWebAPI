using StarFood.Application.Base.Messages;

namespace StarFood.Application.DomainModel.Commands
{
    public class DeleteProductCommand : Command<ICommandResponse>
    {
        public DeleteProductCommand() { }

        public int Id { get; set; }
        public bool IsAvailable { get; set; }
    }
}
