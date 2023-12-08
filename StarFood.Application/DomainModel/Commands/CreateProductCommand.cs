using StarFood.Application.Base.Messages;
using StarFood.Domain.Entities;

namespace StarFood.Domain.Commands
{
    public class CreateProductCommand : Command<ICommandResponse>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? ImgUrl { get; set; }
        public List<Variations>? Variations { get; set; }
    }
}
