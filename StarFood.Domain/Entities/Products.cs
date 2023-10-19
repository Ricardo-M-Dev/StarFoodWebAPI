using System.ComponentModel;

namespace StarFood.Domain.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string ImgUrl { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int RestaurantId { get; set; }
        [DefaultValue(false)]
        public bool IsAvailable { get; set; }

        public Restaurants Restaurant { get; set; }
        public ProductCategories Category { get; set; }
        public List<ProductVariations> ProductsProductVariations { get; set; } = new List<ProductVariations>();

        public Products() { }

        public void SetAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
    }
}
