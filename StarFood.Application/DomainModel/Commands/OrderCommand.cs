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
        [JsonIgnore]
        public int Id { get; set; }
        public List<UpdateProductOrderCommand>? ProductOrders { get; set; }
    }

    public class UpdateProductOrderCommand : Command<ICommandResponse>
    {
        public int Id { get; set; }
        public int VariationId { get; set; }
        public int Quantity { get; set; }
    }

    public class StatusOrderCommand : Command<ICommandResponse>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
    }

    public class DeleteOrderCommand : Command<ICommandResponse>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool Deleted { get; set; }

    }
}
