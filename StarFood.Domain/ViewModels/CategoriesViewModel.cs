namespace StarFood.Domain.ViewModels
{
    public class CategoriesViewModel
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public string? ImgUrl { get; set; }
        public bool IsAvailable { get; set; }
    }
}
