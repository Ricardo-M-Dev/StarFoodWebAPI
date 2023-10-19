using StarFood.Domain.Commands;

namespace StarFood.Application.Models
{
    public class CreateProductModel
    {
        public required CreateProductCommand ProductCommand { get; set; }
        public required List<CreateVariationCommand> VariationCommand { get; set; }
    }
}
