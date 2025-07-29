using AutoMapper;
using Market.Application.DTOs.Address;
using Market.Application.DTOs.User;
using MarketApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Infrastructure.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<User, UserRequest>()
                .ForMember(pr => pr.RoleId, p => p.MapFrom(p => p.RoleId))
            .ReverseMap();

            CreateMap<User, UserUpdateRequest>()
                .ForMember(pr => pr.RoleId, p => p.MapFrom(p => p.RoleId))
                    .ReverseMap();

            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ReverseMap();
        }
    }
}
