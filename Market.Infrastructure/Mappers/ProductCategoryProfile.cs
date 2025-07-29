using AutoMapper;
using Market.Application.DTOs.ProductCategory;
using MarketApi.Models;

namespace Market.Mappers
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategory, ProductCategoryRequest>()
                .ReverseMap();
            CreateMap<ProductCategory, ProductCategoryUpdateRequest>()
                .ReverseMap();
            CreateMap<ProductCategory, ProductCategoryResponse>()
                .ReverseMap();
        }
    }
}
