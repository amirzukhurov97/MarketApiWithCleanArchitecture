using AutoMapper;
using Market.Application.DTOs.Sell;
using MarketApi.Models;

namespace MarketApi.Mappers
{
    public class SaleProfile : Profile
    {
        public SaleProfile() 
        {
            CreateMap<Sell, SellRequest>()
                .ForMember(cr => cr.ProductId, c => c.MapFrom(c => c.ProductId))
                .ForMember(cr => cr.CustomerId, c => c.MapFrom(c => c.CustomerId))
                .ReverseMap();
            CreateMap<Sell, SellUpdateRequest>()
                .ForMember(cr => cr.ProductId, c => c.MapFrom(c => c.ProductId))
                .ForMember(cr => cr.CustomerId, c => c.MapFrom(c => c.CustomerId))
                .ReverseMap();
            CreateMap<Sell, SellResponse>()
                .ForMember(cr => cr.ProductName, c => c.MapFrom(c => c.Product.Name))
                .ForMember(cr => cr.CustomerName, c => c.MapFrom(c => c.Customer.Name))
                .ReverseMap();
        }
    }
}
