using StarFood.Domain.Entities;
using System.ComponentModel;

namespace StarFood.Application.DTOs
{
    public class ProductsDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        [DefaultValue("")]
        public string? ImgUrl { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int RestaurantId { get; set; }
        [DefaultValue(false)]
        public bool IsAvailable { get; set; }
        public Categories? Categories { get; set; }
        public List<Variations> Variations { get; set; }
    }
}
