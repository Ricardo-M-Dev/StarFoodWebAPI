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

        public async Task<Variations> HandleAsync(CreateVariationCommand command)
        {
            if (string.IsNullOrEmpty(command.Description))
            {
                throw new ArgumentException("O nome da variação é obrigatório.");
            }

            var newVariation = new Variations
            {
                Description = command.Description,
                Value = command.Value,
            };

            await _variationRepository.CreateAsync(newVariation);
            return newVariation;
        }
    }
}
