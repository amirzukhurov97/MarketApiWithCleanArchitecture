using Market.Application.DTOs.Address;
using Market.Application.DTOs.Product;
using Market.xUnit.Integration.Configurations;
using MarketApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Market.xUnit.Integration
{
    public class ProductTest : BaseTestEntity
    {
        [Fact]
        public async Task Create_ShouldCreateNewClient()
        {
            var client = CreateHttpClient();
            var firstMeasurementId = await GetFirstMeasurementId();
            var firstProductCategoryId = await GetFirstProductCategorytId();
            var newProduct = new ProductRequest
            {
                Name = "Tests",
                ProductCategoryId = firstProductCategoryId,
                MeasurementId = firstMeasurementId,
            };

            var response = await client.PostAsJsonAsync("/api/Product", newProduct);

            response.EnsureSuccessStatusCode();
            var createdProduct = await response.Content.ReadAsStringAsync();
            Assert.NotNull(createdProduct);
            Assert.Contains(newProduct.Name, createdProduct);
        }


        #region Helper methdos

        private async Task<Guid> GetFirstMeasurementId()
        {
            var client = CreateHttpClient();
            var response = await client.GetAsync("/api/Measurement");
            response.EnsureSuccessStatusCode();
            var measurement = await response.Content.ReadFromJsonAsync<IEnumerable<Measurement>>();
            return measurement!.First().Id;
        }

        private async Task<Guid> GetFirstProductCategorytId()
        {
            var client = CreateHttpClient();
            var response = await client.GetAsync("/api/ProductCategory");
            response.EnsureSuccessStatusCode();
            var productCategory = await response.Content.ReadFromJsonAsync<IEnumerable<ProductCategory>>();
            return productCategory!.First().Id;
        }

        #endregion
    }
}
