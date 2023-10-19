using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

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

        public async Task<List<Variations>> HandleAsyncList(List<CreateVariationCommand> variationList)
        {
            var newVariation = new Variations();
            var newVariationsList = new List<Variations>();

            foreach (var variation in variationList)
            {
                if (string.IsNullOrEmpty(variation.Description))
                {
                    throw new ArgumentException("O nome da variação é obrigatório.");
                }

                newVariation = new Variations
                {
                    Description = variation.Description,
                    Value = variation.Value,
                };

                await _variationRepository.CreateAsync(newVariation);
                newVariationsList.Add(newVariation);
            }

            return newVariationsList;
        }

        public Task<List<Variations>> HandleAsyncList(List<CreateVariationCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
