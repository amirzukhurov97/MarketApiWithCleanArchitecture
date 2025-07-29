using AutoMapper;
using Market.Application.DTOs.CurrencyExchange;
using MarketApi.Models;

namespace MarketApi.Mappers
{
    public class CurrencyExchangeProfile : Profile
    {
        public CurrencyExchangeProfile()
        {
            CreateMap<CurrencyExchange, CurrencyExchangeRequest>()
            .ReverseMap();

            CreateMap<CurrencyExchange, CurrencyExchangeUpdateRequest>()
                    .ReverseMap();

            CreateMap<CurrencyExchange, CurrencyExchangeResponse>()
                .ReverseMap();
        }
    }
}
