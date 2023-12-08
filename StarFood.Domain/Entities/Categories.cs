using System.ComponentModel;

namespace StarFood.Domain.Entities
{
    public class Categories
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public string ImgUrl { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public DateTime CreatedTime { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public DateTime? UpdateTime { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public int RestaurantId { get; set; }
        public bool IsAvailable { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public List<Products> Products { get; set; }
        
        public Categories() { }
    }
}
