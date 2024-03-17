using AutoMapper;
using StarFood.Domain.Entities;
using StarFood.Domain.ViewModels;
using StarFood.Domain.ViewModels.Orders;
using StarFood.Domain.ViewModels.Products;
using StarFood.Domain.ViewModels.Tables;

namespace StarFood.Application.Base.Mappers
{
    public class DomainToViewModelMappingProfiles : Profile
    {
        public DomainToViewModelMappingProfiles() 
        {
            CreateMap<Products, ProductsViewModel>();
            CreateMap<Categories, CategoriesViewModel>();
            CreateMap<Restaurants, RestaurantsViewModel>();
            CreateMap<Variations, VariationViewModel>();
            CreateMap<Orders, OrdersViewModel>();
            CreateMap<ProductOrder, ProductOrderViewModel>();
            CreateMap<Tables, TablesViewModel>();
        }
    }
}
