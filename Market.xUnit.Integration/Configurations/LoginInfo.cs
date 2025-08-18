using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.xUnit.Integration.Configurations
{
    public record LoginInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
