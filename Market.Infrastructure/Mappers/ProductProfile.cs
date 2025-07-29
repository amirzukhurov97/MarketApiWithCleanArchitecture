using AutoMapper;
using Market.Application.DTOs.Product;
using MarketApi.Models;

namespace MarketApi.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile() {
            CreateMap<Product, ProductRequest>()
                .ForMember(pr => pr.ProductCategoryId, p=> p.MapFrom(p => p.ProductCategoryId))
                .ForMember(pr => pr.MeasurementId, p=> p.MapFrom(p => p.MeasurementId))
                .ReverseMap();

            CreateMap<Product, ProductUpdateRequest>()
                .ForMember(pr=>pr.ProductCategoryId, p=>p.MapFrom(p=>p.ProductCategoryId))
                .ForMember(pr=>pr.MeasurementId, p=>p.MapFrom(p=>p.MeasurementId))
                .ReverseMap();

            CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.MeasurementName, opt => opt.MapFrom(src => src.Measurement.Name))
            .ForMember(dest => dest.ProductCategoryName, opt => opt.MapFrom(src => src.ProductCategory.Name))
            .ReverseMap();
        }
    }
}
