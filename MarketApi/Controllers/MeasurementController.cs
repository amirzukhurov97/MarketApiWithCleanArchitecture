using AutoMapper;
using Market.Application.DTOs.Measurement;
using Market.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;

namespace MarketApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementController(IGenericService<MeasurementRequest, MeasurementUpdateRequest, MeasurementResponse> measurementService, ILogger<ProductController> logger) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var measurements = measurementService.GetAll();
                if (measurements is null || !measurements.Any())
                {
                    return NotFound("No measurements found.");
                }
                return Ok(measurements);
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

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public ActionResult<string> Create(MeasurementRequest measurementRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Log.Information("In the method Create request => {@request}", measurementRequest);
                return measurementService.Create(measurementRequest);
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
                var getById = measurementService.GetById(id);
                if (getById is null)
                    return NotFound($"No elements by id {id}");
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
                logger.LogInformation($"Deleting measurement with ID: {id} from the database.");
                var resDel = measurementService.Remove(id);
                return Ok(resDel);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while deleting measurement with ID: {id}.");
                throw new Exception(ex.Message);
            }
        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult Put(MeasurementUpdateRequest measurementUpdate, [FromServices] IMapper mapper)
        {
            try
            {
                logger.LogInformation($"Updating measurement with ID: {measurementUpdate.Id} in the database.");
                var product = measurementService.Update(measurementUpdate);
                return Ok(product);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while updating measurement with ID: {measurementUpdate.Id}.");
                throw new Exception(ex.Message);
            }
        }
    }
}
