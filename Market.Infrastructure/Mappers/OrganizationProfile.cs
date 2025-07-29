using AutoMapper;
using Market.Application.DTOs.Organization;
using MarketApi.Models;

namespace MarketApi.Mappers
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Organization, OrganizationRequest>()
                .ForMember(pr => pr.AddressId, p => p.MapFrom(p => p.AddressId))
                .ForMember(pr => pr.OrganizationTypeId, p => p.MapFrom(p => p.OrganizationTypeId))
                .ReverseMap();

            CreateMap<Organization, OrganizationUpdateRequest>()
                    .ForMember(pr => pr.OrganizationTypeId, p => p.MapFrom(p => p.OrganizationTypeId))
                    .ForMember(pr => pr.AddressId, p => p.MapFrom(p => p.AddressId))
                    .ReverseMap();

            CreateMap<Organization, OrganizationResponse>()
                .ForMember(dest => dest.AddressName, opt => opt.MapFrom(src => src.Address.Name))
                .ForMember(dest => dest.OrganizationTypeName, opt => opt.MapFrom(src => src.OrganizationType.Name))
                .ReverseMap();
        }
    }
}
