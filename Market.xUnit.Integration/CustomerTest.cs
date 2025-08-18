using Market.Application.DTOs.Customer;
using Market.xUnit.Integration.Configurations;
using MarketApi.Models;
using System.Net;
using System.Net.Http.Json;

namespace Market.xUnit.Integration
{
    public class CustomerTest : BaseTestEntity
    {
        [Fact]
        public async Task GetAll_ShouldReturnAllCustomers()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/Customer");

            // Assert
            response.EnsureSuccessStatusCode();
            var clients = await response.Content.ReadFromJsonAsync<IEnumerable<Customer>>();
            Assert.True(clients?.Any() == true);
        }

        [Fact]
        public async Task GetById_ShouldNotReturnCustomerByInvalidId()
        {
            var id = Guid.NewGuid();
            var client = CreateHttpClient();

            var response = await client.GetAsync($"/api/Customer/{id}");

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturnCustomerById()
        {
            var id = await GetFirstCustomerId();
            var client = CreateHttpClient();

            var response = await client.GetAsync($"/api/Customer/{id}");

            response.EnsureSuccessStatusCode();
            var customerData = await response.Content.ReadFromJsonAsync<Customer>();
            Assert.NotNull(customerData);
            Assert.Equal(id, customerData.Id);
        }

        [Fact]
        public async Task Create_ShouldNotCreateNewCustomerWithInvalidData()
        {
            var client = CreateHttpClient();
            var newCustomer = new Customer(); // Missing required fields

            var response = await client.PostAsJsonAsync("/api/Customer", newCustomer);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Create_ShouldCreateNewCustomer()
        {
            var client = CreateHttpClient();
            var addressId = await GetFirstAddressId();
            var newCustomer = new CustomerRequest
            {
                Name = "Tests",
                AddressId = addressId,
                PhoneNumber = "1234567890",
            };

            var response = await client.PostAsJsonAsync("/api/Customer", newCustomer);

            response.EnsureSuccessStatusCode();
            var сustomers = await response.Content.ReadAsStringAsync();
            Assert.NotNull(сustomers);
            Assert.Contains(newCustomer.Name, сustomers);
        }

        [Fact]
        public async Task Update_ShouldNotUpdateByInvalidId()
        {
            var client = CreateHttpClient();
            var сustomerId = Guid.NewGuid();
            var addressId = await GetFirstAddressId();
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

        [Fact]
        public async Task Update_ShouldUpdateByValidId()
        {
            var client = CreateHttpClient();
            var сustomerId = await GetFirstCustomerId();
            var addressId = await GetFirstAddressId();
            var updateCustomer = new CustomerUpdateRequest
            {
                Id = сustomerId,
                Name = "Updated",
                AddressId = addressId,
                PhoneNumber = "0987654321"

            };

            var response = await client.PutAsJsonAsync($"/api/Customer?id={сustomerId}", updateCustomer);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Delete_ShouldNotDeleteCustomerByInvalidId()
        {
            var client = CreateHttpClient();
            var сustomerId = Guid.NewGuid();

            var response = await client.DeleteAsync($"/api/Customer?id={сustomerId}");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Delete_ShouldDeleteCustomerByValidId()
        {
            var client = CreateHttpClient();
            var сustomerId = await GetFirstCustomerId();

            var response = await client.DeleteAsync($"/api/Customer?id={сustomerId}");

            response.EnsureSuccessStatusCode();
        }

        #region Helper methdos

        private async Task<Guid> GetFirstCustomerId()
        {
            var client = CreateHttpClient();
            var response = await client.GetAsync("/api/Customer");
            response.EnsureSuccessStatusCode();
            var customers = await response.Content.ReadFromJsonAsync<IEnumerable<Customer>>();
            return customers!.First().Id;
        }
        private async Task<Guid> GetFirstAddressId()
        {
            var client = CreateHttpClient();
            var response = await client.GetAsync("/api/Address");
            response.EnsureSuccessStatusCode();
            var addresses = await response.Content.ReadFromJsonAsync<IEnumerable<Address>>();
            return addresses!.First().Id;
        }

        #endregion
    }
}
