using AutoMapper;
using Market.Application.DTOs.Role;
using Market.Domain.Models;

namespace Market.Infrastructure.Mappers
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleRequest>()
            .ReverseMap();

            CreateMap<Role, RoleUpdateRequest>()
                    .ReverseMap();

            CreateMap<Role, RoleResponse>()
                .ReverseMap();
        }
    }
}
