using Market.Application.DTOs.Address;
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
    public class AddressController(IGenericService<AddressRequest, AddressUpdateRequest, AddressResponse> addressService, ILogger<AddressController> logger) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var addresses = addressService.GetAll();
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

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public ActionResult<string> Create(AddressRequest addressRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Log.Information("In the method Create request => {@request}", addressRequest);
                var result = addressService.Create(addressRequest);
                return Ok(result);    
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
                var getById = addressService.GetById(id);
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
        //[Authorize(Roles = "admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                logger.LogInformation($"Deleting address with ID: {id} from the database.");
                var resDel = addressService.Remove(id);
                if (resDel is null)
                {
                    logger.LogWarning($"No address found with ID: {id} to delete.");
                    return NotFound(null);
                }
                return Ok(resDel);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while deleting measurement with ID: {id}.");
                throw new Exception(ex.Message);
            }
        }
        [HttpPut]
        //[Authorize(Roles = "admin")]
        public ActionResult<string> Put(AddressUpdateRequest addressUpdate)
        {
            try
            {
                logger.LogInformation($"Updating address with ID: {addressUpdate.Id} in the database.");
                var product = addressService.Update(addressUpdate);
                if (product.Count() == 0)
                    {
                    logger.LogWarning($"No address found with ID: {addressUpdate.Id} to update.");
                    return NotFound($"No address found with ID: {addressUpdate.Id} to update.");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while updating address with ID: {addressUpdate.Id}.");
                throw new Exception(ex.Message);
            }
        }
    }
}
