using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Base;
using StarFood.Application.Base.Messages;
using StarFood.Application.Communication;
using StarFood.Application.Interfaces;
using StarFood.Domain;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Domain.ViewModels.Orders;
using StarsFoodAPI.Services.HttpContext;

[Authorize]
[Route("api/orders/")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IProductsRepository _productsRepository;
    private readonly IVariationsRepository _variationsRepository;
    private readonly IOrdersRepository _orderRespository;
    private readonly IProductOrderRepository _productOrderRepository;
    private readonly IRestaurantsRepository _restaurantRepository;

    public OrdersController(
        IProductsRepository productsRepository,
        IVariationsRepository variationsRepository,
        IOrdersRepository createOrderRepository,
        IProductOrderRepository productOrderRespository,
        IRestaurantsRepository restaurantRepository
        )
    {
        _productsRepository = productsRepository;
        _variationsRepository = variationsRepository;
        _orderRespository = createOrderRepository;
        _productOrderRepository = productOrderRespository;
        _restaurantRepository = restaurantRepository;
    }

    [HttpGet]
    public async Task<ActionResult<OrdersViewModel>> GetAllOrders(
        [FromServices] IOrdersRepository repository,
        [FromServices] IMapper map,
        [FromServices] RequestState requestContext
    )
    {
        try
        {
            Restaurants? restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
            int restaurantId = restaurant.RestaurantId;

            if (restaurant == null)
            {
                return NotFound(new DomainException($"Restaurante de ID {restaurantId} não pode ser encontrado."));
            }

            List<Orders> orders = repository.GetOrdersByRestaurantId(restaurantId);

            if (orders == null || !orders.Any())
            {
                return NotFound(new DomainException($"Nenhum Pedido do Restaurante de ID {restaurantId} foi encontrado."));
            }

            List<OrdersViewModel> ordersViewModels = new List<OrdersViewModel>();

            foreach (Orders order in orders)
            {
                OrdersViewModel orderViewModel = map.Map<OrdersViewModel>(order);
                int orderId = order.Id;

                List<ProductOrderViewModel> productOrderViewModels = new List<ProductOrderViewModel>();

                List<ProductOrder>? productsOrder = _productOrderRepository.GetProductsOrderByOrderId(orderId, restaurantId);

                foreach (ProductOrder productOrder in productsOrder)
                {
                    Variations? variation = _variationsRepository.GetVariationById(productOrder.VariationId, restaurantId);
                    Products? product = _productsRepository.GetProductByVariation(variation.ProductId, restaurantId);

                    ProductOrderViewModel productOrderViewModel = new ProductOrderViewModel
                    {
                        ProductOrderId = productOrder.Id,
                        ProductName = product.Name,
                        VariationName = variation.Name,
                        ProductImg = product.ImgUrl,
                        Quantity = productOrder.Quantity,
                        Value = variation.Value,
                    };

                    productOrderViewModels.Add(productOrderViewModel);
                }

                orderViewModel.ProductOrder = productOrderViewModels;
                ordersViewModels.Add(orderViewModel);
            }

            return Ok(ordersViewModels);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task <ActionResult<OrdersViewModel>> GetOrderById(
        [FromRoute] int id,
        [FromServices] IOrdersRepository repository,
        [FromServices] IMapper map,
        [FromServices] RequestState requestContext

    )
    {
        try
        {
            Restaurants? restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
            int restaurantId = restaurant.RestaurantId;

            if (restaurant == null)
            {
                return NotFound(new DomainException($"Restaurante de ID {restaurantId} não pode ser encontrado."));
            }

            Orders? order = repository.GetOrderById(id, restaurantId);

            if (order == null)
            {
                return NotFound(new DomainException($"Nenhum Pedido de ID {id} foi encontrado."));
            }

            OrdersViewModel orderViewModel = map.Map<OrdersViewModel>(order);

            List<ProductOrder>? productsOrder = _productOrderRepository.GetProductsOrderByOrderId(id, restaurantId);

            List<ProductOrderViewModel> productOrderViewModels = new List<ProductOrderViewModel>();

            foreach (ProductOrder productOrder in productsOrder)
            {
                Variations? variation = _variationsRepository.GetVariationById(productOrder.VariationId, restaurantId);
                Products? product = _productsRepository.GetProductByVariation(variation.ProductId, restaurantId);

                ProductOrderViewModel productOrderViewModel = new ProductOrderViewModel
                {
                    ProductOrderId = productOrder.Id,
                    ProductName = product.Name,
                    VariationName = variation.Name,
                    ProductImg = product.ImgUrl,
                    Quantity = productOrder.Quantity,
                    Value = variation.Value,
                };

                productOrderViewModels.Add(productOrderViewModel);
            }

            orderViewModel.ProductOrder = productOrderViewModels;

            return Ok(orderViewModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderCommand cmd,
        [FromServices] IMediatorHandler mediator,
        [FromServices] IHostApplicationLifetime appLifetime,
        [FromServices] RequestState requestContext
        )
    {
        try
        {
            Restaurants? restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
            int restaurantId = restaurant.RestaurantId;

            if (restaurant == null)
            {
                return NotFound(new DomainException($"Restaurante de ID {restaurantId} não pode ser encontrado."));
            }

            cmd.UpdateRequestInfo(requestContext, restaurant);

            cmd.UserId = requestContext.UserId;

            ICommandResponse result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);
            
            if (result.IsValid)
            {
                return Created($"/api/orders/{result.Id}", result.Object);
            }

            return BadRequest(result.Exception.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(
        [FromRoute] int id,
        [FromBody] UpdateOrderCommand cmd,
        [FromServices] IMediatorHandler mediator,
        [FromServices] IHostApplicationLifetime appLifetime,
        [FromServices] RequestState requestContext
        )
    {
        try
        {
            Restaurants? restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
            int restaurantId = restaurant.RestaurantId;

            if (restaurant == null)
            {
                return NotFound(new DomainException($"Restaurant de ID {restaurantId} não pode ser encontrado."));
            }

            cmd.UpdateRequestInfo(requestContext, restaurant);
            cmd.Id = id;
            cmd.RestaurantId = restaurantId;

            ICommandResponse? result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

            if (result.IsValid)
            {
                return NoContent();
            }

            return BadRequest(result.Exception.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> StatusOrder(
        [FromRoute] int id,
        [FromBody] StatusOrderCommand cmd,
        [FromServices] IMediatorHandler mediator,
        [FromServices] IHostApplicationLifetime appLifetime,
        [FromServices] RequestState requestContext
        )
    {
        try
        {
            Restaurants? restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
            int restaurantId = restaurant.RestaurantId;

            if (restaurant == null)
            {
                return NotFound(new DomainException($"Restaurant de ID {restaurantId} não pode ser encontrado."));
            }

            cmd.UpdateRequestInfo(requestContext, restaurant);
            cmd.Id = id;
            cmd.RestaurantId = restaurantId;

            ICommandResponse? result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

            if (result.IsValid)
            {
                return Ok(result.Object);

            }

            return BadRequest(result.Exception.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(
        [FromRoute] int id,
        [FromBody] DeleteOrderCommand cmd,
        [FromServices] IMediatorHandler mediator,
        [FromServices] IHostApplicationLifetime appLifetime,
        [FromServices] RequestState requestContext
    )
    {
        try
        {
            Restaurants? restaurant = _restaurantRepository.GetRestaurantById(requestContext.RestaurantId);
            int restaurantId = restaurant.RestaurantId;

            if (restaurant == null)
            {
                return BadRequest(new DomainException($"Restaurant de ID {restaurantId} não pode ser encontrado."));
            }

            cmd.Id = id;
            cmd.RestaurantId = restaurantId;

            ICommandResponse result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

            if (result.IsValid)
            {
                return NoContent();
            }

            return BadRequest(result.Exception.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
