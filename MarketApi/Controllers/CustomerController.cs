using Market.Application.DTOs.Customer;
using Market.Application.Services;
using Market.Application.SeviceInterfacies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CustomerController(ICustomerService<CustomerRequest, CustomerUpdateRequest, CustomerResponse> customerService, ILogger<CustomerController> logger) : ControllerBase
    {        

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var customers = customerService.GetAll();
                if (customers is null || !customers.Any())
                {
                    return NotFound("No customers found.");
                }
                return Ok(customers);

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

        [HttpGet("GetByAlphabet")]
        public IActionResult GetByAlphabet()
        {
            try
            {
                var customers = customerService.GetByAlphabet();
                if (customers is null || !customers.Any())
                {
                    return NotFound("No customers found.");
                }
                return Ok(customers);

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
                var result = customerService.GetAll(pageSize, pageNumber);
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


        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var getById = customerService.GetById(id);
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

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public ActionResult<string> Create(CustomerRequest customerRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Log.Information("In the method Create request => {@request}", customerRequest);
                return customerService.Create(customerRequest);
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

        [HttpDelete]
        //[Authorize(Roles = "admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                logger.LogInformation($"Deleting customer with ID: {id} from the database.");
                var resDel = customerService.Remove(id);
                if (resDel.IsNullOrEmpty())
                {
                    logger.LogWarning($"No customer found with ID: {id} to delete.");
                    return NotFound($"No customer found with ID: {id}");
                }
                return Ok(resDel);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while deleting customer with ID: {id}.");
                throw new Exception(ex.Message);
            }
        }
        [HttpPut]
        //[Authorize(Roles = "admin")]
        public IActionResult Put(CustomerUpdateRequest customerUpdate)
        {
            try
            {
                logger.LogInformation($"Updating customer with ID: {customerUpdate.Id} in the database.");
                var product = customerService.Update(customerUpdate);
                if (product.IsNullOrEmpty())
                {
                    logger.LogWarning($"No customer found with ID: {customerUpdate.Id} to update.");
                    return NotFound($"No customer found with ID: {customerUpdate.Id}");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while updating customer with ID: {customerUpdate.Id}.");
                throw new Exception(ex.Message);
            }
        }
    }
}
