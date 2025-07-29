using AutoMapper;
using Market.Application.DTOs.Sale;
using MarketApi.Models;

namespace MarketApi.Mappers
{
    public class SaleProfile : Profile
    {
        public SaleProfile() 
        {
            CreateMap<Sale, SaleRequest>()
                .ForMember(cr => cr.ProductId, c => c.MapFrom(c => c.ProductId))
                .ForMember(cr => cr.CustomerId, c => c.MapFrom(c => c.CustomerId))
                .ReverseMap();
            CreateMap<Sale, SaleUpdateRequest>()
                .ForMember(cr => cr.ProductId, c => c.MapFrom(c => c.ProductId))
                .ForMember(cr => cr.CustomerId, c => c.MapFrom(c => c.CustomerId))
                .ReverseMap();
            CreateMap<Sale, SaleResponse>()
                .ForMember(cr => cr.ProductName, c => c.MapFrom(c => c.Product.Name))
                .ForMember(cr => cr.CustomerName, c => c.MapFrom(c => c.Customer.Name))
                .ReverseMap();
        }
    }
}
