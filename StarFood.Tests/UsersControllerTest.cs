using Microsoft.AspNetCore.Mvc;
using Moq;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarsFoodAPI.Controllers;
using StarsFoodAPI.Services.HttpContext;

namespace StarFood.Tests
{
    public class UsersControllerTest
    {
        [Fact]
        public async Task GetUserById_ReturnsOkObjectResult()
        {
            // Arrange
            var userId = 1;
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCreateUserCommandHandler = new Mock<ICommandHandler<CreateUserCommand, Users>>();
            var mockUpdateUserCommandHandler = new Mock<ICommandHandler<UpdateUserCommand, Users>>();
            var mockAuthenticatedContext = new Mock<AuthenticatedContext>();

            mockAuthenticatedContext.SetupGet(x => x.RestaurantId).Returns(1);

            var controller = new UsersController(
                mockUserRepository.Object,
                mockCreateUserCommandHandler.Object,
                mockUpdateUserCommandHandler.Object
            );

            // Act
            var result = await controller.GetUserById(mockAuthenticatedContext.Object, userId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Users>(result.Value);
        }
    }
}