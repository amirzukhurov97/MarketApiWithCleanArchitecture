using Market.Domain.Abstract.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.DTOs.User
{
    public record UserRequest : EntityBaseRequest
    {
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }
        public Guid RoleId { get; set; }
    }
}
