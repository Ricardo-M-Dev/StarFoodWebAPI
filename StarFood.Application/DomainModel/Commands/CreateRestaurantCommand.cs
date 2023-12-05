namespace StarFood.Domain.Commands
{
    public class CreateRestaurantCommand
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
