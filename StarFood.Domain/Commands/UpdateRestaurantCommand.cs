
namespace StarFood.Domain.Commands
{
    public class UpdateRestaurantCommand
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}
