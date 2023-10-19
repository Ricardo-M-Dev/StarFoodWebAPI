namespace StarFood.Application.Models
{
    public class ProductCategoryModel
    {
        public string CategoryName { get; set; } = string.Empty;
        public int RestaurantId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
