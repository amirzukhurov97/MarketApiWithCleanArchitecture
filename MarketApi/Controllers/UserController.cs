using Market.Application.DTOs.User;
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
    public class UserController(IGenericService<UserRequest, UserUpdateRequest, UserResponse> service, ILogger<UserController> logger) : ControllerBase
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
        public ActionResult<string> Create(UserRequest Request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Log.Information("In the method Create request => {@request}", Request);
                return service.Create(Request);
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
                logger.LogInformation($"Deleting User with ID: {id} from the database.");
                var resDel = service.Remove(id);
                return Ok(resDel);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while deleting User with ID: {id}.");
                throw new Exception(ex.Message);
            }
        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult Put(UserUpdateRequest roleUpdate)
        {
            try
            {
                logger.LogInformation($"Updating User with ID: {roleUpdate.Id} in the database.");
                var role = service.Update(roleUpdate);
                return Ok(role);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while updating User with ID: {roleUpdate.Id}.");
                throw new Exception(ex.Message);
            }
        }
    }
}
