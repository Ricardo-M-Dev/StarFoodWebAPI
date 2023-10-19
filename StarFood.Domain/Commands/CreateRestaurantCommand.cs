namespace StarFood.Domain.Commands
{
    public class CreateRestaurantCommand
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
