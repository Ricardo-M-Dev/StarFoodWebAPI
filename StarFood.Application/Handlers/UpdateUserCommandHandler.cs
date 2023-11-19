using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;

namespace StarFood.Application.Handlers
{
    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Users>
    {
        private readonly StarFoodDbContext _context;
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(StarFoodDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<Users> HandleAsync(UpdateUserCommand command, int restaurantId)
        {
            var user = await _context.Users.FindAsync(command.Id);

            if (user == null)
            {
                return user;
            }
            else
            {
                user.Username = command.Username;
                user.Password = command.Password;
                user.Alias = command.Alias;
                user.RestaurantId = restaurantId;
                user.IsActive = command.IsActive;

                await _userRepository.UpdateAsync(command.Id, user);
                return user;
            }
        }

        public Task<List<Users>> HandleAsyncList(List<UpdateUserCommand> commandList, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
