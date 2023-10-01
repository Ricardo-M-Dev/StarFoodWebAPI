namespace StarFood.Domain.Commands
{
    public class CreateRestaurantCommand
    {
        public int Id { get; set; }
        public string RestaurantId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
