using StarFood.Domain.Entities;

namespace StarFood.Domain.ViewModels
{
    public class ProductVariationViewModel
    {
        public ProductVariationViewModel() { }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public string? ImgUrl { get; set; }
        public bool IsAvailable { get; set; }
        public List<Variations> Variations { get; set; }
    }
}
