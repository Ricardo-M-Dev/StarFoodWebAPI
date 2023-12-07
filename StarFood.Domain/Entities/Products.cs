using System.ComponentModel;

namespace StarFood.Domain.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int RestaurantId { get; set; }
        public bool IsAvailable { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public Categories? Category { get; set; }
        public List<Variations> Variations { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public List<OrderProducts> OrderProducts { get; set; }

        public Products() { }
    }
}
