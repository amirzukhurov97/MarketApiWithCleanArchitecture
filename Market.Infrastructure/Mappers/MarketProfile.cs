using AutoMapper;
using Market.Application.DTOs.Market;
using MarketApi.Models;

namespace MarketApi.Mappers
{
    public class MarketProfile : Profile
    {
        public MarketProfile()
        {
            CreateMap<Stock, MarketRequest>()
                .ForMember(cr => cr.ProductId, c => c.MapFrom(c => c.ProductId))
                .ReverseMap();
            CreateMap<Stock, MarketUpdateRequest>()
                .ForMember(cr => cr.ProductId, c => c.MapFrom(c => c.ProductId))
                .ReverseMap();
            CreateMap<Stock, MarketResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ReverseMap();
        }
    }
}
