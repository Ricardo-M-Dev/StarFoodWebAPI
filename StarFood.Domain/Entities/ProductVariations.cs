using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace StarFood.Domain.Entities
{
    public class ProductVariations
    {
        public int Id { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DefaultValue(0.00)]
        public decimal Value { get; set; }
        public int RestaurantId { get; set; }
        [DefaultValue(false)]
        public bool IsAvailable { get; set; }

        public Restaurants Restaurant { get; set; }
        public List<DishesProductVariations> DishesProductVariations { get; set; } = new List<DishesProductVariations>();

        public ProductVariations() { }

        public ProductVariations(int id, string description, decimal value, bool isAvailable = true)
        {
            Id = id;
            Description = description;
            Value = value;
            IsAvailable = isAvailable;
        }

        public void Update(string description, decimal value)
        {
            Description = description;
            Value = value;
        }

        public void SetAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
    }

}
