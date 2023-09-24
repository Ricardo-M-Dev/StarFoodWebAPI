using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarFood.Application.Models
{
    public class DishesProductVariationsModel
    {
        public int DishId { get; set; }
        public int VariationId { get; set; }
        public int RestaurantId { get; set; }
    }
}
