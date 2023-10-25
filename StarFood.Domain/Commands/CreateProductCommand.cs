namespace StarFood.Domain.Commands
{
    public class CreateProductCommand
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; }
        public int CategoryId { get; set; }
        public int RestaurantId { get; set; }
    }
}
