namespace StarFood.Domain.Commands
{
    public class UpdateCategoryCommand
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public DateTime UpdateTime { get; set; }
        public int RestaurantId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
