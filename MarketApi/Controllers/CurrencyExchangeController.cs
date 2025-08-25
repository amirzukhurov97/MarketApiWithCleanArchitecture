using Market.Application.DTOs.CurrencyExchange;
using Market.Application.Services;
using MarketApi.Infrastructure.Interfacies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;

namespace MarketApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyExchangeController(IGenericService<CurrencyExchangeRequest, CurrencyExchangeUpdateRequest, CurrencyExchangeResponse> service, ICurrencyExchangeRepository currencyExchange, ILogger<PurchaseController> logger) : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Create(CurrencyExchangeRequest сurrencyExchangeRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Log.Information("In the method Create request => {@request}", сurrencyExchangeRequest);
                return service.Create(сurrencyExchangeRequest);
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

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var сurrencyExchanges = service.GetAll();
                if (сurrencyExchanges is null || !сurrencyExchanges.Any())
                {
                    return NotFound("No сurrencyExchanges found.");
                }
                return Ok(сurrencyExchanges);

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

        [HttpGet("pagination")]
        public IActionResult GetAll(int pageSize, int pageNumber)
        {
            try
            {
                var addresses = service.GetAll(pageSize, pageNumber);
                if (addresses is null || !addresses.Any())
                {
                    return NotFound("No addresses found.");
                }
                return Ok(addresses);
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

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var getById = service.GetById(id);
                if (getById is null)
                    return NotFound("You have not data");
                Log.Information("In the method GetById result=>{@getById}", getById);
                return Ok(getById);
            }
            catch (SqlException ex)
            {
                Log.Error("SQL Error in GetById method: {@ex}", ex);
                return StatusCode(500, $"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error("Exception in GetById method: {@ex}", ex);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                logger.LogInformation($"Deleting CurrencyExchange with ID: {id} from the database.");
                var resDel = service.Remove(id);
                return Ok(resDel);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while deleting CurrencyExchange with ID: {id}.");
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("actualCurrency")]
        public IActionResult ActualCurrency()
        {
            try
            {
                logger.LogInformation($"Getting actual CurrencyExchange from the database.");
                var resDel = currencyExchange.GetActual();
                return Ok(resDel);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while getting CurrencyExchange.");
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(CurrencyExchangeUpdateRequest currencyExchangeUpdate)
        {
            try
            {
                logger.LogInformation($"Updating CurrencyExchange with ID: {currencyExchangeUpdate.Id} in the database.");
                var product = service.Update(currencyExchangeUpdate);
                return Ok(product);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while updating CurrencyExchange with ID: {currencyExchangeUpdate.Id}.");
                throw new Exception(ex.Message);
            }
        }
    }
}
