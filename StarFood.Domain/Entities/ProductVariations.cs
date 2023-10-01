namespace StarFood.Domain.Entities
{
    public class ProductVariations
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Products Product { get; set; } = new Products();

        public int VariationId { get; set; }
        public Variations Variation { get; set; } = new Variations();

        public int RestaurantId { get; set; }
        public Restaurants Restaurant { get; set; } = new Restaurants();

        public void Update(int productId, int variationId)
        {
            ProductId = productId;
            VariationId = variationId;
        }
    }

}
