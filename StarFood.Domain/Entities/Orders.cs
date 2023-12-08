using System.Text.Json.Serialization;

namespace StarFood.Domain.Entities
{
    public class Orders
    {
        public int Id { get; set; }
        public int Table { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? Waiter { get; set; }
        public int RestaurantId { get; set; }
        public List<OrderProducts> OrderProducts { get; set; }

        public Orders() { }
    }
}
