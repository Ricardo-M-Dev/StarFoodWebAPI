using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace StarFood.Domain.Entities
{
    public class Variations
    {
        public int Id { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DefaultValue(0.00)]
        public decimal Value { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int RestaurantId { get; set; }
        [DefaultValue(false)]
        public bool IsAvailable { get; set; }

        public Restaurants Restaurant { get; set; }
        public List<ProductVariations> ProductsProductVariations { get; set; } = new List<ProductVariations>();

        public Variations() { }

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
