using AutoMapper;
using StarFood.Domain.Entities;
using StarFood.Domain.ViewModels;

namespace StarFood.Application.Base.Mappers
{
    public class DomainToViewModelMappingProfiles : Profile
    {
        public DomainToViewModelMappingProfiles() 
        {
            CreateMap<Products, ProductsViewModel>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Categories
             {
                 Id = src.Category.Id,
                 CategoryName = src.Category.CategoryName,
                 IsAvailable = src.Category.IsAvailable
             }))
            .ForMember(dest => dest.Variations, opt => opt.MapFrom(src => src.Variations));

            CreateMap<Categories, CategoriesViewModel>();
            CreateMap<Restaurants, RestaurantsViewModel>();
        }
    }
}
