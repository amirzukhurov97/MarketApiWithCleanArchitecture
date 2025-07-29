using Market.Domain.Abstract.Entity;
using Market.Domain.Models;

namespace MarketApi.Models
{
    public class User : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public bool IsBlocked { get; set; }
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
