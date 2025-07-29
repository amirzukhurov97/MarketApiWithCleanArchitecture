using AutoMapper;
using Market.Application.Authentication;
using Market.Application.DTOs.Auth;
using Market.Application.Interfacies;
using Market.Domain.Models;
using MarketApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Market.Application.Services
{
    public class AuthService(IUserRepository repository, IMapper mapper)
    {
        public async Task<AuthSessionToken> Login(string login, string password)
        {
            var auth = repository.GetAll().FirstOrDefault(k => k.Email == login && k.Password == password);
            if (auth != null)
            {
                return await GeneratedJWt(auth);
            }
            return null;
        }

        public async Task<AuthSessionToken> RefreshToken(string refreshToken)
        {
            var user = repository.GetAll().FirstOrDefault(k => k.RefreshToken == refreshToken);

            if (user is null)
            {
                throw new ArgumentException("Invalid Email and Password");
            }
            else
            {
                return await GeneratedJWt(user);
            }
        }

        public string Create(AuthRequest auth)
        {
            if (string.IsNullOrEmpty(auth.Login))
            {
                return "The name cannot be empty";
            }
            else
            {
                var mapuser = mapper.Map<User>(auth);
                repository.Add(mapuser);
                return $"Created new item with this ID: {mapuser.Id}";
            }
        }
        private async Task<AuthSessionToken> GeneratedJWt(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id",user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role.Name),
            };

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                claims: claims,
                expires: DateTime.Now.Add(TimeSpan.FromMinutes(AuthOptions.lifeTime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            var refreshToken = Guid.NewGuid().ToString();

            user.RefreshToken = refreshToken;
            repository.Update(user);
            return new AuthSessionToken
            {
                AccessToken = accessToken,
                Role = user.Role.Name,
                RefreshToken = refreshToken
            };
        }
    }
}
