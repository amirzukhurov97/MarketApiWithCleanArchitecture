using Market.Application.DTOs.Product;
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
    public class ProductController(IGenericService<ProductRequest, ProductUpdateRequest, ProductResponse> productServise, ILogger<ProductController> logger) : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var products = productServise.GetAll();
                if (products is null || !products.Any())
                {
                    return NotFound("No products found.");
                }
                return Ok(products);

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
                var getById = productServise.GetById(id);
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

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public ActionResult<string> Create(ProductRequest productRequest) {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Log.Information("In the method Create request => {@request}", productRequest);
                return productServise.Create(productRequest);
            }
            catch (SqlException ex)
            {
                Log.Error("SQL Error in Create method: {@ex}", ex);
                return StatusCode(500, $"Database error: {ex.Message}");
            }
            catch (Exception ex) {
                Log.Error("Exception in Create method: {@ex}", ex);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {                
                logger.LogInformation($"Deleting product with ID: {id} from the database.");
                var resDel = productServise.Remove(id);                
                return Ok(resDel);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while deleting product with ID: {id}.");
                throw new Exception(ex.Message);
            }        
        }
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult Put(ProductUpdateRequest productUpdate) 
        {
            try
            {
                logger.LogInformation($"Updating product with ID: {productUpdate.Id} in the database.");
                var product = productServise.Update(productUpdate);
                return Ok(product);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while updating product with ID: {productUpdate.Id}.");
                throw new Exception(ex.Message);
            }
        }
    }
}