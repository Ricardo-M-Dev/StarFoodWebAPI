using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;
using StarsFoodAPI.Services.HttpContext;

[Authorize]
[Route("api")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ICommandHandler<CreateOrderCommand, Orders> _createOrderCommand;
    private readonly ICommandHandler<CreateOrderProductsCommand, OrderProducts> _createOrderProductsCommand;

    public OrdersController(
        ICommandHandler<CreateOrderCommand, Orders> createOrderCommand,
        ICommandHandler<CreateOrderProductsCommand, OrderProducts> createOrderProductsCommand)
    {
        _createOrderCommand = createOrderCommand;
        _createOrderProductsCommand = createOrderProductsCommand;
    }

    [HttpPost("CreateOrder")]
    public async Task<IActionResult> CreateOrder(
        [FromServices] RequestState auth,
        [FromBody] CreateOrderCommand createOrderCommand)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var newOrder = await _createOrderCommand.HandleAsync(createOrderCommand, restaurantId);

            if (newOrder != null)
            {
                return Ok();
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

    [HttpPost("AddProducts")]
    public async Task<IActionResult> AddProducts(
        [FromServices] RequestState auth,
        [FromBody] List<CreateOrderProductsCommand> createOrderProductsCommand)
    {
        try
        {
            var restaurantId = auth.RestaurantId;
            var newOrderProducts = await _createOrderProductsCommand.HandleAsyncList(createOrderProductsCommand, restaurantId);

            if (newOrderProducts != null)
            {
                return Ok();
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
