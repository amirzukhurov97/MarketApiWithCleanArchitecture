using AutoMapper;
using Market.Application.DTOs.ReturnOrganization;
using MarketApi.Models;

namespace MarketApi.Mappers
{
    public class ReturnOrganizationProfile : Profile
    {
        public ReturnOrganizationProfile()
        {
            CreateMap<ReturnOrganization, ReturnOrganizationRequest>()
                .ForMember(cr => cr.ProductId, c => c.MapFrom(c => c.ProductId))
                .ForMember(cr => cr.OrganizationId, c => c.MapFrom(c => c.OrganizationId))
                .ReverseMap();
            CreateMap<ReturnOrganization, ReturnOrganizationResponse>()
                .ForMember(cr => cr.ProductName, c => c.MapFrom(c => c.Product.Name))
                .ForMember(cr => cr.OrganizationName, c => c.MapFrom(c => c.Organization.Name))
                .ReverseMap();
        }
    }
}
