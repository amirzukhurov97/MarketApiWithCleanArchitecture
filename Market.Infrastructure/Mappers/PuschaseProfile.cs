using AutoMapper;
using Market.Application.DTOs.Purchase;
using MarketApi.Models;

namespace MarketApi.Mappers
{
    public class PuschaseProfile : Profile
    {
        public PuschaseProfile()
        {
            CreateMap<Purchase, PurchaseRequest>()
                .ForMember(pr => pr.ProductId, p => p.MapFrom(p => p.ProductId))
                .ForMember(pr => pr.OrganizationId, p => p.MapFrom(p => p.OrganizationId))
                .ReverseMap();
            CreateMap<Purchase, PurchaseUpdateRequest>()
                .ForMember(pr => pr.ProductId, p => p.MapFrom(p => p.ProductId))
                .ForMember(pr => pr.OrganizationId, p => p.MapFrom(p => p.OrganizationId))
                .ReverseMap();
            CreateMap<Purchase, PurchaseResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.Organization.Name))
                .ReverseMap();
        }
    }
}
