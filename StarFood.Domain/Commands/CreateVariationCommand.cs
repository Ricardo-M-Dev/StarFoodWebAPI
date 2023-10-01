namespace StarFood.Domain.Commands
{
    public class CreateVariationCommand
    {
        public int Id { get; private set; }
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string RestaurantId { get; set; } = string.Empty;
    }
}
