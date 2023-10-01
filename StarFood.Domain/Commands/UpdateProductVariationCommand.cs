namespace StarFood.Domain.Commands
{
    public class UpdateProductVariationCommand
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int VariationId { get; set; }
    }
}
