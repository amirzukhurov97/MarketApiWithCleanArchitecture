using Market.Application.DTOs.Report;
using Market.Application.DTOs.ReturnOrganization;
using Market.Application.Services;
using Market.Application.SeviceInterfacies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;

namespace MarketApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReturnOrganizationController(IReturnOrganizationService<ReturnOrganizationRequest, ReturnOrganizationUpdateRequest, ReturnOrganizationResponse> service, ILogger<ReturnOrganizationController> logger) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<string> Create([FromBody] ReturnOrganizationRequest productRequest)
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
                    return NotFound("No Sell found.");
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
                var result = service.GetAll(pageSize, pageNumber);
                if (result is null || !result.Any())
                {
                    return NotFound("No elements found.");
                }
                return Ok(result);
            }
            catch (SqlException ex)
            {
                Log.Error("SQL Error in Pagination method: {@ex}", ex);
                return StatusCode(500, $"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error("Exception in Pagination method: {@ex}", ex);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetReport")]
        public IActionResult GetReport([FromQuery] ReportModel reportModel)
        {
            try
            {
                var purchases = service.GetReport(reportModel);
                if (purchases is null || !purchases.Any())
                {
                    return NotFound("There is no record with such parameters");
                }
                return Ok(purchases);

            }
            catch (SqlException ex)
            {
                Log.Error("SQL Error in GetReport method: {@ex}", ex);
                return StatusCode(500, $"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error("Exception in GetReport method: {@ex}", ex);
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
                logger.LogInformation($"Deleting Sell with ID: {id} from the database.");
                var resDel = service.Remove(id);
                return Ok(resDel);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while deleting Sell with ID: {id}.");
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult Put([FromBody] ReturnOrganizationUpdateRequest saleUpdate)
        {
            try
            {
                logger.LogInformation($"Updating Sell with ID: {saleUpdate.Id} in the database.");
                var product = service.Update(saleUpdate);
                return Ok(product);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while updating Sell with ID: {saleUpdate.Id}.");
                throw new Exception(ex.Message);
            }
        }
    }
}
