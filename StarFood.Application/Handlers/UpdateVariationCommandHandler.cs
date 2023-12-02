using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;

namespace StarFood.Application.CommandHandlers
{
    public class UpdateVariationCommandHandler : ICommandHandler<UpdateVariationCommand, Variations>
    {
        private readonly StarFoodDbContext _context;
        private readonly IVariationsRepository _variationRepository;

        public UpdateVariationCommandHandler(StarFoodDbContext context, IVariationsRepository variationRepository)
        {
            _context = context;
            _variationRepository = variationRepository;
        }

        public Task<Variations> HandleAsync(UpdateVariationCommand command, int restaurantId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Variations>> HandleAsyncList(List<UpdateVariationCommand> commandList, int restaurantId)
        {
            List<Variations> updatedVariations = new();

            foreach (var variation in commandList)
            {
                if (string.IsNullOrEmpty(variation.Description))
                {
                    throw new ArgumentException("O nome da variação é obrigatório.");
                }

                var updateVariation = await _context.Variations.FindAsync(variation.Id);

                if (updateVariation != null)
                {
                    updateVariation.Description = variation.Description;
                    updateVariation.ProductId = variation.ProductId;
                    updateVariation.UpdateTime = DateTime.Now;
                    updateVariation.Value = variation.Value;
                    updateVariation.IsAvailable = variation.IsAvailable;

                    await _variationRepository.UpdateAsync(variation.Id, updateVariation);

                    updatedVariations.Add(updateVariation);
                }
                else
                {
                    Variations newVariation = new();
                    newVariation.Description = variation.Description;
                    newVariation.ProductId = variation.ProductId;
                    newVariation.UpdateTime = DateTime.Now;
                    newVariation.Value = variation.Value;
                    newVariation.IsAvailable = variation.IsAvailable;

                    await _variationRepository.CreateAsync(newVariation);

                    updatedVariations.Add(newVariation);
                }
            }

            return updatedVariations;
        }
    }
}
