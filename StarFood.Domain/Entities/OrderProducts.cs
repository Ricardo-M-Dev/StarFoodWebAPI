namespace StarFood.Domain.Entities
{
    public class OrderProducts
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int VariationId { get; set; }
        public int Quantity { get; set; }
        public OrderStatus Status { get; set; }
        public Orders Orders { get; set; }
        public Products Products { get; set; }

        public OrderProducts() { }
    }
}
