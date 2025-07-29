using Market.Domain.Abstract.Entity;
using MarketApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Models
{
    public class Auth : EntityBase
    {
        public string Login { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public bool IsBlocked { get; set; }

        public User? User { get; set; }
        public Guid UserId { get; set; }
    }
}
