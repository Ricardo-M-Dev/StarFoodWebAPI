using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;

namespace StarFood.Application.Handlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Users>
    {
        private readonly StarFoodDbContext _context;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(StarFoodDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<Users> HandleAsync(CreateUserCommand command, int restaurantId)
        {
            var user = await _userRepository.GetByUsernameAsync(command.Username);
            if (user == null) 
            {
                var newUser = new Users
                {
                    Username = command.Username,
                    Password = command.Password,
                    Alias = command.Alias,
                    Role = command.Role,
                    RestaurantId = restaurantId,
                    IsActive = command.IsActive,
                };

                await _userRepository.CreateAsync(newUser);

                return newUser;
            }
            else
            {
                return user;
            }
        }

        public async Task<List<Users>> HandleAsyncList(List<CreateUserCommand> commandList, int restaurantId)
        {
            var users = new List<Users>();

            foreach (var command in commandList)
            {
                var user = new Users
                {

                };
                await _userRepository.CreateAsync(user);
                users.Add(user);
            }

            return users;
        }
    }
}
