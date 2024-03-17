
namespace StarFood.Domain.ViewModels.Products
{
    /// <summary>
    /// ViewModel de Produto(s).
    /// </summary>
    public class ProductsViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
        public List<VariationViewModel>? Variations { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int RestaurantId { get; set; }
        public CategoriesViewModel? Category { get; set; }
        public bool Status { get; set; }
        public bool Active { get; set; }
    }

    /// <summary>
    /// ViewModel de Variações.
    /// </summary>
    public class VariationViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int RestaurantId { get; set; }
        public int ProductId { get; set; }
        public bool Active { get; set; }
    }

    /// <summary>
    /// ViewModel de Categoria(s)
    /// </summary>
    public class CategoriesViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
