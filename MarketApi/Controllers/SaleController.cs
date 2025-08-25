using Market.Application.DTOs.Sale;
using Market.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;

namespace MarketApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController(IGenericService<SaleRequest, SaleUpdateRequest, SaleResponse> service, ILogger<SaleController> logger) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<string> Create([FromBody]SaleRequest productRequest)
        {
            try
            {                
                Log.Information("In the method Create request => {@request}", productRequest);
                return service.Create(productRequest);
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
                var sales = service.GetAll();
                if (sales is null || !sales.Any())
                {
                    return NotFound("No Sale found.");
                }
                return Ok(sales);

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
                var resultPagination = service.GetAll(pageSize, pageNumber);
                if (resultPagination is null || !resultPagination.Any())
                {
                    return NotFound("No resultPagination found.");
                }
                return Ok(resultPagination);
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
        [Authorize(Roles = "admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                logger.LogInformation($"Deleting Sale with ID: {id} from the database.");
                var resDel = service.Remove(id);
                return Ok(resDel);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while deleting Sale with ID: {id}.");
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult Put([FromBody]SaleUpdateRequest saleUpdate)
        {
            try
            {
                logger.LogInformation($"Updating Sale with ID: {saleUpdate.Id} in the database.");
                var product = service.Update(saleUpdate);
                return Ok(product);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while updating Sale with ID: {saleUpdate.Id}.");
                throw new Exception(ex.Message);
            }
        }
    }
}
