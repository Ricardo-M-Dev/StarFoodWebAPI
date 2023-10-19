using System.ComponentModel;

namespace StarFood.Domain.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductTypeId { get; set; }
        public int CategoryId { get; set; }
        public int RestaurantId { get; set; }
        [DefaultValue(false)]
        public bool IsAvailable { get; set; }

        public Restaurants Restaurant { get; set; }
        public ProductCategories Category { get; set; }
        public List<ProductVariations> ProductsProductVariations { get; set; } = new List<ProductVariations>();

        public Products() { }

        public Products(int id, string name, string description, int typeId, int categoryId, int restaurantId, bool isAvailable = true)
        {
            Id = id;
            Name = name;
            Description = description;
            ProductTypeId = typeId;
            CategoryId = categoryId;
            RestaurantId = restaurantId;
            IsAvailable = isAvailable;
        }

        public void Update(string name, string description, int typeId, int categoryId, bool isAvailable)
        {
            Name = name;
            Description = description;
            ProductTypeId = typeId;
            CategoryId = categoryId;
            IsAvailable = isAvailable;
        }

        public void SetAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
    }
}
