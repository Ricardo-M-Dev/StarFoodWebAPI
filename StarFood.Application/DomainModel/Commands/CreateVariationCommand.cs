namespace StarFood.Domain.Commands
{
    public class CreateVariationCommand
    {
        public int Id { get; private set; }
        public string Description { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public DateTime CreatedTime { get; set; }
        public decimal Value { get; set; }
        public int RestaurantId { get; set; }
    }
}
