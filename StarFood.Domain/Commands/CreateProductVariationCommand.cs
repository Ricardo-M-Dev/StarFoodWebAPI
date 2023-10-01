namespace StarFood.Domain.Commands
{
    public class CreateProductVariationCommand
    {
        public int ProductId { get; set; }
        public int VariationId { get; set; }
        public int RestaurantId { get; set; }
    }
}
