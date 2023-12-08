namespace StarFood.Domain.Commands
{
    public class UpdateOrderCommand
    {
        public int Id { get; set; }
        public int Table { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Waiter { get; set; }
        public OrderStatus Status { get; set; }
    }
}
