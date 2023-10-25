using Microsoft.EntityFrameworkCore;
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

        public async Task<Variations> HandleAsync(UpdateVariationCommand command, int restaurantId)
        {
            if (string.IsNullOrEmpty(command.Description))
            {
                throw new ArgumentException("O nome da variação é obrigatório.");
            }

            var variation = _context.Variations.Find(command.Id);

            if (variation == null)
            {
                return variation;
            }
            else
            {
                variation.Description = command.Description;
                variation.ProductId = command.ProductId;
                variation.UpdateTime = DateTime.Now;
                variation.Value = command.Value;
                variation.IsAvailable = command.IsAvailable;
            }

            await _variationRepository.UpdateAsync(command.Id, variation);
            return variation;
        }

        public Task<List<Variations>> HandleAsyncList(List<UpdateVariationCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
