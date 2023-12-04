using StarFood.Domain.Entities;

namespace StarFood.Domain.ViewModels
{
    public class ProductCategoryVariationViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? ImgUrl { get; set; }
        public bool IsAvailable { get; set; }
        public Categories? Categories { get; set; }
        public List<Variations> Variations { get; set; }
    }
}
