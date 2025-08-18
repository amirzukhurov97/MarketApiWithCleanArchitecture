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
        public async Task<IActionResult> LogIn(string username, string password)
        {
            var token = authService.Login(username, password);
            if (token == null)
            {
                return NotFound("Invalid Email and Password");
            }
            return Ok(token);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var token = authService.RefreshToken(refreshToken);
            if (token == null)
            {
                return NotFound("Invalid Refresh Token");
            }
            return Ok(token);
        }
    }
}
