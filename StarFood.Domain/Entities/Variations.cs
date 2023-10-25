using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace StarFood.Domain.Entities
{
    public class Variations
    {
        public int Id { get; private set; }
        public string Description { get; set; }
        public int ProductId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DefaultValue(0.00)]
        public decimal Value { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
        public int RestaurantId { get; set; }
        [DefaultValue(false)]
        public bool IsAvailable { get; set; }

        public Products Products { get; set; }
        public Variations() { }
    }

}
