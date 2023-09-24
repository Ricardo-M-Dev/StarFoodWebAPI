namespace StarFood.Domain.Commands
{
    public class CreateProductTypeCommand
    {
        public string TypeName { get; set; }
        public int RestaurantId { get; set; }
    }
}
