using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Application.CommandHandlers
{
    public class CreateVariationCommandHandler : ICommandHandler<CreateVariationCommand, Variations>
    {
        private readonly IVariationsRepository _variationRepository;

        public CreateVariationCommandHandler(IVariationsRepository variationRepository)
        {
            _variationRepository = variationRepository;
        }

        public Task<Variations> HandleAsync(CreateVariationCommand command, int restaurantId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Variations>> HandleAsyncList(List<CreateVariationCommand> variationList, int restaurantId)
        {
            List<Variations>? newVariationsList = new();

            foreach (var variation in variationList)
            {
                if (string.IsNullOrEmpty(variation.Description))
                {
                    throw new ArgumentException("O nome da variação é obrigatório.");
                }

                Variations? newVariation = new Variations
                { 
                    Description = variation.Description,
                    ProductId = variation.ProductId,
                    Value = variation.Value,
                    CreatedTime = DateTime.Now,
                    RestaurantId = restaurantId
                };

                await _variationRepository.CreateAsync(newVariation);
                newVariationsList.Add(newVariation);
            }

            return newVariationsList;
        }
    }
}
