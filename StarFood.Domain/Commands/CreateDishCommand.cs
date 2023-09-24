using StarFood.Domain.Entities;

namespace StarFood.Domain.Commands
{
    public class CreateDishCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductTypeId { get; set; }
        public int CategoryId { get; set; }
        public int RestaurantId { get; set; }
    }
}
