using StarFood.Application.Base.Messages;
using StarFood.Domain.Entities;
using System.Text.Json.Serialization;

namespace StarFood.Domain.Commands
{
    public class CreateOrderCommand : Command<ICommandResponse>
    {
        public int TableId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        public List<CreateProductOrder>? ProductsOrder { get; set; }
    }

    public class CreateProductOrder : Command<ICommandResponse>
    {
        [JsonIgnore]
        public int OrderId { get; set; }
        public int VariationId { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateOrderCommand : Command<ICommandResponse>
    {
        public int Id { get; set; }
        public bool Paid { get; set; }

        public List<ProductOrder>? ProductOrders { get; set; }
    }
}
