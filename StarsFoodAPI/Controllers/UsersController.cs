using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarFood.Application.CommandHandlers;
using StarFood.Application.Handlers;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;
using StarsFoodAPI.Services.HttpContext;

namespace StarsFoodAPI.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly StarFoodDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly CreateUserCommandHandler _createUserCommandHandle;
        private readonly UpdateUserCommandHandler _updateUserCommandHandle;

        public UsersController(StarFoodDbContext context, IUserRepository userRepository, CreateUserCommandHandler createUserCommandHandler, UpdateUserCommandHandler updateUserCommandHandler)
        {
            _context = context;
            _userRepository = userRepository;
            _createUserCommandHandle = createUserCommandHandler;
            _updateUserCommandHandle = updateUserCommandHandler;
        }


        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUserById([FromServices] AuthenticatedContext auth, int id)
        {
            try
            {
                var restaurantId = auth.RestaurantId;
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null) return NotFound();
                else
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(
            [FromServices] AuthenticatedContext auth,
            [FromBody] CreateUserCommand createUserCommand)
        {
            try
            {
                var restaurantId = auth.RestaurantId;
                var newUser = await _createUserCommandHandle.HandleAsync(createUserCommand, restaurantId);
                if (newUser == null) return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPatch("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(
        [FromRoute] int id,
        [FromServices] AuthenticatedContext auth,
        [FromBody] UpdateUserCommand updateUserCommand
        )
        {
            try
            {
                var restaurantId = auth.RestaurantId;
                updateUserCommand.Id = id;
                var updateUser = await _updateUserCommandHandle.HandleAsync(updateUserCommand, restaurantId);

                if (updateUser != null)
                {
                    return Ok(updateUser);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
