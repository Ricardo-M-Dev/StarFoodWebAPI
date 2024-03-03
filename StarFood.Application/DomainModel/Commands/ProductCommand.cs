using StarFood.Application.Base.Messages;
using System.Text.Json.Serialization;

namespace StarFood.Domain.Commands
{
    public class CreateProductCommand : Command<ICommandResponse>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
        public int CategoryId { get; set; }
        public List<CreateVariations>? Variations { get; set; }
    }

    public class CreateVariations : Command<ICommandResponse>
    {
        public string? Name { get; set; }
        public decimal Value { get; set; }
    }

    public class UpdateProductCommand : Command<ICommandResponse>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
        public int CategoryId { get; set; }
        public List<UpdateVariationCommand>? Variations { get; set; }
    }

    public class UpdateVariationCommand : Command<ICommandResponse>
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Value { get; set; }
    }

    public class StatusProductCommand : Command<ICommandResponse>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool Status { get; set; }
    }

    public class DeleteProductCommand : Command<ICommandResponse>
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}
