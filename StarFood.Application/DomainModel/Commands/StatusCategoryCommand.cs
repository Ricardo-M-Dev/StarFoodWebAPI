using StarFood.Application.Base.Messages;

namespace StarFood.Application.DomainModel.Commands
{
    public class StatusCategoryCommand : Command<ICommandResponse>
    {
        public StatusCategoryCommand() { }

        public int Id { get; set; }
        public bool IsAvailable { get; set; }
    }
}
