using Newtonsoft.Json;
using StarFood.Domain.Entities;

namespace StarFood.Domain.ViewModels
{
    public class ProductsViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
        [JsonProperty("createdTime")]
        public DateTime CreatedTime { get; set; }
        [JsonProperty("updateTime")]
        public DateTime? UpdateTime { get; set; }
        public int CategoryId { get; set; }
        public bool IsAvailable { get; set; }
        public bool Active { get; set; }
        public Categories? Category { get; set; }
        public List<Variations>? Variations { get; set; }
    }
}
