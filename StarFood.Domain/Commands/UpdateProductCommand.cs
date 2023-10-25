namespace StarFood.Domain.Commands
{
    public class UpdateProductCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdateTime { get; set; }
        public int CategoryId { get; set; }
        public int RestaurantId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
