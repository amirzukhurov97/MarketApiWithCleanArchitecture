using Market.Application.DTOs.Customer;
using Market.Application.DTOs.Purchase;
using Market.xUnit.Integration.Configurations;
using MarketApi.Models;
using System.Net;
using System.Net.Http.Json;

namespace Market.xUnit.Integration
{
    public class PurchaseTest : BaseTestEntity
    {
        [Fact]
        public async Task GetAll_ShouldReturnAllPurchase()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/Purchase");

            // Assert
            response.EnsureSuccessStatusCode();
            var clients = await response.Content.ReadFromJsonAsync<IEnumerable<Purchase>>();
            Assert.True(clients?.Any() == true);
        }

        /*[Fact]
        public async Task GetById_ShouldNotReturnCustomerByInvalidId()
        {
            var id = Guid.NewGuid();
            var client = CreateHttpClient();

            var response = await client.GetAsync($"/api/Customer/{id}");

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

<<<<<<< HEAD
        //[Fact]
        //public async Task GetById_ShouldReturnCustomerById()
        //{
        //    var id = await GetFirstCustomerId();
        //    var client = CreateHttpClient();
=======
        /*Fact]
        public async Task GetById_ShouldReturnCustomerById()
        {
            var id = await GetFirstCustomerId();
            var client = CreateHttpClient();
>>>>>>> c65d3cde40603de45b7199c2dd280e3a6e8c30fd

        //    var response = await client.GetAsync($"/api/Customer/{id}");

<<<<<<< HEAD
        //    response.EnsureSuccessStatusCode();
        //    var customerData = await response.Content.ReadFromJsonAsync<Customer>();
        //    Assert.NotNull(customerData);
        //    Assert.Equal(id, customerData.Id);
        //}
=======
            response.EnsureSuccessStatusCode();
            var customerData = await response.Content.ReadFromJsonAsync<Customer>();
            Assert.NotNull(customerData);
            Assert.Equal(id, customerData.Id);
        }*/
>>>>>>> c65d3cde40603de45b7199c2dd280e3a6e8c30fd

        [Fact]
        public async Task Create_ShouldNotCreateNewPurchaseWithInvalidData()
        {
            var client = CreateHttpClient();
            var newCustomer = new Customer(); // Missing required fields

            var response = await client.PostAsJsonAsync("/api/Purchase", newCustomer);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Create_ShouldCreateNewPurchase()
        {
            var client = CreateHttpClient();
            var newPurchase = new PurchaseRequest
            {
                ProductId = await GetFirstProductId(),
                OrganizationId = await GetFirstOrganizationId(),
                Price = 150,
                PriceUSD = 15,
                Quantity = 10,
<<<<<<< HEAD
=======
                //Date = DateTime.UtcNow,
>>>>>>> c65d3cde40603de45b7199c2dd280e3a6e8c30fd
                Comment = "Test purchase"
            };

            var response = await client.PostAsJsonAsync("/api/Purchase", newPurchase);

            response.EnsureSuccessStatusCode();
            var purchases = await response.Content.ReadAsStringAsync();
            Assert.NotNull(purchases);
            Assert.Contains(newPurchase.ProductId.ToString(), purchases);
        }

        [Fact]
        public async Task Update_ShouldNotUpdateByInvalidId()
        {
            var client = CreateHttpClient();
            var сustomerId = Guid.NewGuid();
            var addressId = await GetFirstProductId();
            var updateCustomer = new CustomerUpdateRequest
            {
                Id = сustomerId,
                Name = "Updated",
                AddressId = addressId,
                PhoneNumber = "0987654321"

            };

            var response = await client.PutAsJsonAsync($"/api/Customer?id={сustomerId}", updateCustomer);

            Assert.False(response.IsSuccessStatusCode);
        }

<<<<<<< HEAD
        //[Fact]
        //public async Task Update_ShouldUpdateByValidId()
        //{
        //    var client = CreateHttpClient();
        //    var сustomerId = await GetFirstCustomerId();
        //    var addressId = await GetFirstProductId();
        //    var updateCustomer = new CustomerUpdateRequest
        //    {
        //        Id = сustomerId,
        //        Name = "Updated",
        //        AddressId = addressId,
        //        PhoneNumber = "0987654321"
=======
        /*[Fact]
        public async Task Update_ShouldUpdateByValidId()
        {
            var client = CreateHttpClient();
            var сustomerId = await GetFirstCustomerId();
            var addressId = await GetFirstProductId();
            var updateCustomer = new CustomerUpdateRequest
            {
                Id = сustomerId,
                Name = "Updated",
                AddressId = addressId,
                PhoneNumber = "0987654321"
>>>>>>> c65d3cde40603de45b7199c2dd280e3a6e8c30fd

        //    };

        //    var response = await client.PutAsJsonAsync($"/api/Customer?id={сustomerId}", updateCustomer);

        //    response.EnsureSuccessStatusCode();
        //}

        /*[Fact]
        public async Task Delete_ShouldNotDeleteCustomerByInvalidId()
        {
            var client = CreateHttpClient();
            var сustomerId = Guid.NewGuid();

            var response = await client.DeleteAsync($"/api/Customer?id={сustomerId}");

            Assert.False(response.IsSuccessStatusCode);
        }*/

<<<<<<< HEAD
        //[Fact]
        //public async Task Delete_ShouldDeleteCustomerByValidId()
        //{
        //    var client = CreateHttpClient();
        //    var сustomerId = await GetFirstCustomerId();
=======
        /*[Fact]
        public async Task Delete_ShouldDeleteCustomerByValidId()
        {
            var client = CreateHttpClient();
            var сustomerId = await GetFirstCustomerId();
>>>>>>> c65d3cde40603de45b7199c2dd280e3a6e8c30fd

        //    var response = await client.DeleteAsync($"/api/Customer?id={сustomerId}");

<<<<<<< HEAD
        //    response.EnsureSuccessStatusCode();
        //}

        #region Helper methdos

        //private async Task<Guid> GetFirstCustomerId()
        //{
        //    var client = CreateHttpClient();
        //    var response = await client.GetAsync("/api/Purchase");
        //    response.EnsureSuccessStatusCode();
        //    var purchases = await response.Content.ReadFromJsonAsync<IEnumerable<Purchase>>();
        //    return purchases!.First().Id;
        //}
=======
            response.EnsureSuccessStatusCode();
        }*/

        #region Helper methdos

        /*private async Task<Guid> GetFirstCustomerId()
        {
            var client = CreateHttpClient();
            var response = await client.GetAsync("/api/Purchase");
            response.EnsureSuccessStatusCode();
            var purchases = await response.Content.ReadFromJsonAsync<IEnumerable<Purchase>>();
            return purchases!.First().Id;
        }*/
>>>>>>> c65d3cde40603de45b7199c2dd280e3a6e8c30fd
        private async Task<Guid> GetFirstProductId()
        {
            var client = CreateHttpClient();
            var response = await client.GetAsync("/api/Product");
            response.EnsureSuccessStatusCode();
            var product = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
            return product!.First().Id;
        }
        private async Task<Guid> GetFirstOrganizationId()
        {
            var client = CreateHttpClient();
            var response = await client.GetAsync("/api/Organization");
            response.EnsureSuccessStatusCode();
            var product = await response.Content.ReadFromJsonAsync<IEnumerable<Organization>>();
            return product!.First().Id;
        }

        #endregion
    }
}
