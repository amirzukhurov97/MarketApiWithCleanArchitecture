using Market.Domain.Abstract.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.DTOs.User
{
    public record UserUpdateRequest : EntityBaseUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }
        public Guid RoleId { get; set; }
    }
}
