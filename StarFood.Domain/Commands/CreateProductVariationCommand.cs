namespace StarFood.Domain.Commands
{
    public class CreateProductVariationCommand
    {
        public CreateProductCommand createProductCommand {  get; set; }
        public List<CreateVariationCommand> createVariationCommandList { get; set; }
    }
}
