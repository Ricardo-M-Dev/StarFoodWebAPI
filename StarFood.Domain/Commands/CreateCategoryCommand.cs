namespace StarFood.Domain.Commands
{
    public class CreateCategoryCommand
    {
        public string CategoryName { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; }
        public int RestaurantId { get; set; }
    }
}
