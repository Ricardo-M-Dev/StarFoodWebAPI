
using StarFood.Domain.Commands;

namespace StarFood.Application.Models
{
    public  class UpdateProductModel
    {
        public required UpdateProductCommand ProductCommand { get; set; }
        public required UpdateVariationCommand VariationCommand { get; set; }
    }
}
