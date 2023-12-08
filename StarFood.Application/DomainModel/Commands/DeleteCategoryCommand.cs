using StarFood.Application.Base.Messages;

namespace StarFood.Application.DomainModel.Commands
{
    public class DeleteCategoryCommand : Command<ICommandResponse>
    {
        public DeleteCategoryCommand() { }

        public int Id { get; set; }
    }
}
