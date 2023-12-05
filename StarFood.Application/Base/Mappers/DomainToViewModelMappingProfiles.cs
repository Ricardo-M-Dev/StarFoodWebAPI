using AutoMapper;
using StarFood.Domain.Entities;
using StarFood.Domain.ViewModels;

namespace StarFood.Application.Base.Mappers
{
    public class DomainToViewModelMappingProfiles : Profile
    {
        public DomainToViewModelMappingProfiles() 
        {
            CreateMap<Products, ProductCategoryVariationViewModel>();
        }
    }
}
