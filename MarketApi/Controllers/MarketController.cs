using Market.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;

namespace MarketApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketController(MarketService service, ILogger<MarketController> logger) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var markets = service.GetAll();
                if (markets is null || !markets.Any())
                {
                    return NotFound("No markets found.");
                }
                return Ok(markets);

            }
            catch (SqlException ex)
            {
                Log.Error("SQL Error in Create method: {@ex}", ex);
                return StatusCode(500, $"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error("Exception in Create method: {@ex}", ex);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
