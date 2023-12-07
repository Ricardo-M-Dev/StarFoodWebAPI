using StarFood.Application.Base.Messages;

namespace StarFood.Domain.Commands
{
    public class CreateCategoryCommand : Command<ICommandResponse>
    {
        public string CategoryName { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;
    }
}
