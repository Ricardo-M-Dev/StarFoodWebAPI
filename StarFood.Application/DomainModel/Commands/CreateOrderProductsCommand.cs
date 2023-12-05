namespace StarFood.Domain.Commands
{
    public class CreateOrderProductsCommand
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int VariationId { get; set; }
        public int Quantity { get; set; }
        public OrderStatus Status { get; set; }

    }
}
