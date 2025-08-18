using Market.Application.DTOs.Address;
using Market.Application.DTOs.ProductCategory;
using Market.xUnit.Integration.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Market.xUnit.Integration
{
    public class ProductCategoryTest : BaseTestEntity
    {
        [Fact]
        public async Task Create_ShouldCreateNewProductCategory()
        {
            var client = CreateHttpClient();
            var newProductCategory = new ProductCategoryRequest
            {
                Name = "TestProductCategory"
            };

            var response = await client.PostAsJsonAsync("/api/ProductCategory", newProductCategory);

            response.EnsureSuccessStatusCode();
            var createdProductCategory = await response.Content.ReadAsStringAsync();
            Assert.NotNull(createdProductCategory);
            Assert.Contains(newProductCategory.Name, createdProductCategory);
        }
    }
}
