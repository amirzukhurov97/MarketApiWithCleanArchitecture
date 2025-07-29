using Market.Application.DTOs.Auth;
using Market.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AuthService authService) : ControllerBase
    {
        [HttpPost("LogIn")]
        public async Task<AuthSessionToken> LogIn(string username, string password)
        {
            var token = await authService.Login(username, password);
            return token;
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var token = await authService.RefreshToken(refreshToken);
            return Ok(token);
        }
    }
}
