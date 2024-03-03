namespace StarFood.Domain.ViewModels.Orders
{
    public class OrdersViewModel
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public bool Paid { get; set; }
        public List<ProductOrderViewModel>? ProductOrder { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int RestaurantId { get; set; }
        public int TableId { get; set; }
        public int UserId { get; set; }
        public bool Active { get; set; }
    }

    public class ProductOrderViewModel
    {
        public int ProductOrderId { get; set; }
        public string ProductName { get; set; }
        public string VariationName { get; set; }
        public string? ProductImg { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
    }
}
