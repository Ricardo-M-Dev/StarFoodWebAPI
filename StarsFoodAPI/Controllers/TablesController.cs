using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Base;
using StarFood.Application.Base.Messages;
using StarFood.Application.Communication;
using StarFood.Application.DomainModel.Commands;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Domain.ViewModels.Tables;
using StarsFoodAPI.Services.HttpContext;

[Authorize]
[Route("api/tables/")]
[ApiController]
public class TablesController : ControllerBase
{
    private readonly IRestaurantsRepository _restaurantRepository;
    private readonly ITablesRepository _tablesRepository;

    public TablesController(
        IRestaurantsRepository restaurantsRepository, ITablesRepository tablesRepository)
    {
        _restaurantRepository = restaurantsRepository;
        _tablesRepository = tablesRepository;
    }

    [HttpGet]
    public async Task<ActionResult<TablesViewModel>> GetAllTables(
        [FromServices] ITablesRepository repository,
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

            List<Tables>? tables = repository.GetByRestaurantId(restaurantId);

            if (tables == null || !tables.Any())
            {
                return NotFound(new DomainException($"Nenhuma Mesa do Restaurante de ID {restaurantId} foi encontrado."));
            }

            List<TablesViewModel>? result = map.Map<List<TablesViewModel>>(tables);

            return Ok(result.ToArray().OrderBy(t => t.Number));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TablesViewModel>> GetTableById(
        [FromRoute] int id,
        [FromServices] ITablesRepository repository,
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
            Tables? table = repository.GetTableById(id, restaurantId);

            if (table == null)
            {
                return NotFound(new DomainException($"Nenhuma Mesa de ID {id} foi encontrada."));
            }
            else
            {
                TablesViewModel? result = map.Map<TablesViewModel>(table);

                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("barcode/{barcode}")]
    public async Task<ActionResult<TablesViewModel>> GetByBarcode(
        [FromRoute] string barcode,
        [FromServices] ITablesRepository repository,
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
            Tables? table = repository.GetByBarcode(barcode, restaurantId);

            if (table == null)
            {
                return NotFound(new DomainException($"Nenhuma Mesa com Código de Barras: {barcode} foi encontrada."));
            }
            else
            {
                TablesViewModel? result = map.Map<TablesViewModel>(table);

                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("number/{number}")]
    public async Task<ActionResult<TablesViewModel>> GetByNumber(
        [FromRoute] int number,
        [FromServices] ITablesRepository repository,
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
            Tables? table = repository.GetByNumber(number, restaurantId);

            if (table == null)
            {
                return NotFound(new DomainException($"Nenhuma Mesa de Número {number} foi encontrada."));
            }
            else
            {
                TablesViewModel? result = map.Map<TablesViewModel>(table);

                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTable(
        [FromBody] CreateTableCommand cmd,
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

            cmd.UpdateRequestInfo(requestContext, restaurant);

            ICommandResponse result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

            if (result.IsValid)
            {
                return Created($"/api/categories/{result.Id}", result.Object);
            }
            else
            {
                return BadRequest(result.Exception.Message);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTable(
        [FromRoute] int id,
        [FromBody] UpdateTableCommand cmd,
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

            cmd.UpdateRequestInfo(requestContext, restaurant);
            cmd.Id = id;
            ICommandResponse result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

            if (result.IsValid)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Exception.Message);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTable(
        [FromRoute] int id,
        [FromBody] DeleteTableCommand cmd,
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
