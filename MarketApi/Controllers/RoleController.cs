using Market.Application.DTOs.Address;
using Market.Application.DTOs.Role;
using Market.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;

namespace Market.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController(IGenericService<RoleRequest, RoleUpdateRequest, RoleResponse> service, ILogger<RoleController> logger) : ControllerBase
    {
        [HttpGet]

        public IActionResult GetAll()
        {
            try
            {
                var addresses = service.GetAll();
                if (addresses is null || !addresses.Any())
                {
                    return NotFound("No Role found.");
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
        [Authorize(Roles = "admin")]
        public ActionResult<string> Create(RoleRequest roleRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Log.Information("In the method Create request => {@request}", roleRequest);
                return service.Create(roleRequest);
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
                logger.LogInformation($"Deleting Role with ID: {id} from the database.");
                var resDel = service.Remove(id);
                return Ok(resDel);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while deleting Role with ID: {id}.");
                throw new Exception(ex.Message);
            }
        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult Put(RoleUpdateRequest roleUpdate)
        {
            try
            {
                logger.LogInformation($"Updating Role with ID: {roleUpdate.Id} in the database.");
                var role = service.Update(roleUpdate);
                return Ok(role);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while updating Role with ID: {roleUpdate.Id}.");
                throw new Exception(ex.Message);
            }
        }
    }
}
