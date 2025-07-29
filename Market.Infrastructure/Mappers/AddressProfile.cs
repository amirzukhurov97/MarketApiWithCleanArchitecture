using AutoMapper;
using Market.Application.DTOs.Address;
using MarketApi.Models;

namespace MarketApi.Mappers
{
    public class AddressProfile : Profile
    {

        public AddressProfile()
        {
            CreateMap<Address, AddressRequest>()
            .ReverseMap();

            CreateMap<Address, AddressUpdateRequest>()
                    .ReverseMap();

            CreateMap<Address, AddressResponse>()
                .ReverseMap();
        }
    }
}
