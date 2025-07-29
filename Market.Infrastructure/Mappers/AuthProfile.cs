using AutoMapper;
using Market.Application.DTOs.Auth;
using Market.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Infrastructure.Mappers
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AuthRequest, Auth>();
        }
    }
}
