using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Application.Authentication
{
    public class AuthOptions
    {
        public const string Issuer = "MarketIsser";
        public const string Audience = "MyAuthUser";
        const string key = "1B306BA1-11BB-4866-916B-47D12F6FB0A2";
        public const int lifeTime = 30;
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }
}
