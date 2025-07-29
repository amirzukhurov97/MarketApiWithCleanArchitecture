using AutoMapper;
using Market.Application.DTOs.OrganizationType;
using MarketApi.Models;

namespace MarketApi.Mappers
{
    public class OrganizationTypeProfile : Profile
    {

        public OrganizationTypeProfile()
        {
            CreateMap<OrganizationType, OrganizationTypeRequest>()
            .ReverseMap();

            CreateMap<OrganizationType, OrganizationTypeUpdateRequest>()
                    .ReverseMap();

            CreateMap<OrganizationType, OrganizationTypeResponse>()
                .ReverseMap();
        }
    }
}
