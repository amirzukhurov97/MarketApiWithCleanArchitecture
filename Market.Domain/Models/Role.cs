using Market.Domain.Abstract.Entity;
using MarketApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Models
{
    public class Role : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public List<User>? User { get; set; } = new List<User>();   
    }
}
