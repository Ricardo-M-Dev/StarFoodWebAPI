using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarFood.Application.Base;
using StarFood.Application.Base.Messages;
using StarFood.Application.Communication;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Domain.ViewModels.Products;
using StarsFoodAPI.Services.HttpContext;

[Authorize]
[Route("api/products/")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IVariationsRepository _variationsRepository;
    private readonly IRestaurantsRepository _restaurantRepository;

    public ProductsController(
        ICategoriesRepository categoriesRepository,
        IVariationsRepository variationsRepository,
        IRestaurantsRepository restaurantsRepository
    )
    {
        _categoriesRepository = categoriesRepository;
        _variationsRepository = variationsRepository;
        _restaurantRepository = restaurantsRepository;
    }

    [HttpGet]
    public async Task<ActionResult<ProductsViewModel>> GetAllProducts(
        [FromServices] IProductsRepository repository,
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

            List<Products>? products = repository.GetProductsByRestaurantId(restaurantId);

            if (products == null || !products.Any())
            {
                return NotFound(new DomainException($"Nenhum Produto do Restaurante de ID {restaurantId} foi encontrado."));
            }

            List<ProductsViewModel> productsViewModel = new List<ProductsViewModel>();

            foreach (Products product in products)
            {
                ProductsViewModel productViewModel = map.Map<ProductsViewModel>(product);
                int productId = product.Id;

                List<Variations> variations = _variationsRepository.GetVariationsByProductId(productId, restaurantId);

                productViewModel.Variations = variations.Select(v => new VariationViewModel
                {
                    Id = v.Id,
                    Name = v.Name,
                    Value = v.Value,
                    CreatedAt = v.CreatedDate,
                    UpdatedAt = v.UpdatedDate,
                    DeletedAt = v.DeletedDate,
                    RestaurantId = restaurantId,
                }).ToList();

                int categoryId = product.CategoryId;

                productViewModel.Category = map.Map<CategoriesViewModel>(_categoriesRepository.GetCategoryById(restaurantId, categoryId));

                productsViewModel.Add(productViewModel);
            }

            return Ok(productsViewModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task <ActionResult<ProductsViewModel>> GetProductById(
        [FromRoute] int id,
        [FromServices] IProductsRepository repository,
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

            Products? product = repository.GetProductById(id, restaurantId);

            if (product == null)
            {
                return NotFound(new DomainException($"Nenhum Produto de ID {id} foi encontrado."));
            }

            ProductsViewModel productViewModel = map.Map<ProductsViewModel>(product);

            int categoryId = product.CategoryId;
            productViewModel.Category = map.Map<CategoriesViewModel>(_categoriesRepository.GetCategoryById(restaurantId, categoryId));

            int productId = product.Id;

            List<Variations> variations = _variationsRepository.GetVariationsByProductId(productId, restaurantId);

            productViewModel.Variations = variations.Select(v => new VariationViewModel
            {
                Id = v.Id,
                Name = v.Name,
                Value = v.Value,
                CreatedAt = v.CreatedDate,
                UpdatedAt = v.UpdatedDate,
                DeletedAt = v.DeletedDate,
                RestaurantId = restaurantId,
            }).ToList();

            return Ok(productViewModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(
        [FromBody] CreateProductCommand cmd,
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

            restaurantId = requestContext.RestaurantId;

            ICommandResponse result = await mediator.SendCommand(cmd, appLifetime.ApplicationStopping);

            if (result.IsValid)
            {
                return Created($"/api/products/{result.Id}", result.Object);
            }

            return BadRequest(result.Exception.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] int id,
        [FromBody] UpdateProductCommand cmd,
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
    public async Task<IActionResult> SetStatus(
        [FromRoute] int id,
        [FromBody] StatusProductCommand cmd,
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(
        [FromBody] DeleteProductCommand cmd,
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