namespace StarFood.Application.CommandHandlers
{
    public class CreateRestaurantCommandHandler
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
