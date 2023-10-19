using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;

namespace StarFood.Application.CommandHandlers
{
    public class UpdateVariationCommandHandler : ICommandHandler<UpdateVariationCommand, Variations>
    {
        private readonly IVariationsRepository _variationRepository;

        public UpdateVariationCommandHandler(IVariationsRepository variationRepository)
        {
            _variationRepository = variationRepository;
        }

        public async Task<Variations> HandleAsync(UpdateVariationCommand command)
        {
            if (string.IsNullOrEmpty(command.Description))
            {
                throw new ArgumentException("O nome da variação é obrigatório.");
            }

            var updateVariation = new Variations
            {
                Description = command.Description,
                Value = command.Value
            };

            await _variationRepository.UpdateAsync(command.Id, updateVariation);
            return updateVariation;
        }

        public Task<Variations> HandleAsync(UpdateVariationCommand command, int restaurantId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Variations>> HandleAsyncList(List<UpdateVariationCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
