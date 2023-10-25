using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarFood.Domain.Commands
{
    public class UpdateVariationCommand
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime UpdateTime { get; set; }
        public decimal Value { get; set; }
        public int RestaurantId { get; set; }
    }
}
