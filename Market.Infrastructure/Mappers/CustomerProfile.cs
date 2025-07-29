using AutoMapper;
using Market.Application.DTOs.Customer;
using MarketApi.Models;

namespace MarketApi.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerRequest>()
                .ForMember(cr => cr.AddressId, c => c.MapFrom(c => c.AddressId))
                .ReverseMap();
            CreateMap<Customer, CustomerUpdateRequest>()
                .ForMember(cr => cr.AddressId, c => c.MapFrom(c => c.AddressId))
                .ReverseMap();
            CreateMap<Customer, CustomerResponse>()
                .ForMember(dest => dest.AddressName, opt => opt.MapFrom(src => src.Address.Name))
                .ReverseMap();
        }
    }
}
