namespace StarFood.Domain.Commands
{
    public class CreateOrderCommand
    {
        public int Table { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Waiter { get; set; }
        public List<int> ProductsId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
