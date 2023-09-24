namespace StarFood.Domain.Entities
{
    public class DishesProductVariations
    {
        public int Id { get; set; }

        public int DishesId { get; set; }
        public Dishes Dishes { get; set; }

        public int ProductVariationId { get; set; }
        public ProductVariations ProductVariation { get; set; }

        public int RestaurantId { get; set; }
        public Restaurants Restaurant { get; set; }

        public void Update(int dishId, int variationId)
        {
            DishesId = dishId;
            ProductVariationId = variationId;
        }
    }

}
