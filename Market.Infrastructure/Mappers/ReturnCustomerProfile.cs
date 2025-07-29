using AutoMapper;
using Market.Application.DTOs.ReturnCustomer;
using MarketApi.Models;

namespace MarketApi.Mappers
{
    public class ReturnCustomerProfile : Profile
    {
        public ReturnCustomerProfile()
        {
            CreateMap<ReturnCustomer, ReturnCustomerRequest>()
                .ForMember(cr => cr.ProductId, c => c.MapFrom(c => c.ProductId))
                .ForMember(cr => cr.CustomerId, c => c.MapFrom(c => c.CustomerId))
                .ReverseMap();
            CreateMap<ReturnCustomer, ReturnCustomerResponse>()
                .ForMember(cr => cr.ProductName, c => c.MapFrom(c => c.Product.Name))
                .ForMember(cr => cr.CustomerName, c => c.MapFrom(c => c.Customer.Name))
                .ReverseMap();
        }
    }
}
