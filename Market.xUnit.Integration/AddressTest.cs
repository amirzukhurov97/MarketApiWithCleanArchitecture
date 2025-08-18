using Market.Application.DTOs.Address;
using Market.xUnit.Integration.Configurations;
using MarketApi.Models;
using System.Net;
using System.Net.Http.Json;

namespace Market.xUnit.Integration
{
    public class AddressTest : BaseTestEntity
    {
        [Fact]
        public async Task GetAll_ShouldReturnAllAddreses()
        {
            // Arrange
            var client = CreateHttpClient();

            // Act
            var response = await client.GetAsync("/api/Address");

            // Assert
            response.EnsureSuccessStatusCode();
            var addresses = await response.Content.ReadFromJsonAsync<IEnumerable<Address>>();
            Assert.True(addresses?.Any() == true);
        }

        [Fact]
        public async Task GetById_ShouldNotReturnAddreseByInvalidId()
        {
            var id = Guid.NewGuid();
            var client = CreateHttpClient();

            var response = await client.GetAsync($"/api/Address/{id}");

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturnAddreseById()
        {
            var id = await GetFirstAddressId();
            var client = CreateHttpClient();

            var response = await client.GetAsync($"/api/Address/{id}");

            response.EnsureSuccessStatusCode();
            var adressData = await response.Content.ReadFromJsonAsync<Address>();
            Assert.NotNull(adressData);
            Assert.Equal(id, adressData.Id);
        }

        [Fact]
        public async Task Create_ShouldNotCreateNewAddreseWithInvalidData()
        {
            var client = CreateHttpClient();
            var newAddress = new Address(); // Missing required fields

            var response = await client.PostAsJsonAsync("/api/Address", newAddress);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Create_ShouldCreateNewClient()
        {
            var client = CreateHttpClient();
            var newAddress = new AddressRequest
            {
                Name = "Tests"
            };

            var response = await client.PostAsJsonAsync("/api/Address", newAddress);

            response.EnsureSuccessStatusCode();
            var createdAddress = await response.Content.ReadAsStringAsync();
            Assert.NotNull(createdAddress);
            Assert.Contains(newAddress.Name, createdAddress);
        }

        [Fact]
        public async Task Update_ShouldNotUpdateByInvalidId()
        {
            var client = CreateHttpClient();
            var addressId = Guid.NewGuid();
            var updateAddress = new AddressUpdateRequest
            {
                Id = addressId,
                Name = "Updated",
             
            };

            var response = await client.PutAsJsonAsync($"/api/Address?id={addressId}", updateAddress);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Update_ShouldUpdateByValidId()
        {
            var client = CreateHttpClient();
            var addressId = await GetFirstAddressId();
            var updateAddress = new AddressUpdateRequest
            {
                Id = addressId,
                Name = "Updated"
            };

            var response = await client.PutAsJsonAsync($"/api/Address?id={addressId}", updateAddress);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Delete_ShouldNotDeleteClientByInvalidId()
        {
            var client = CreateHttpClient();
            var addressId = Guid.NewGuid();

            var response = await client.DeleteAsync($"/api/Address?id={addressId}");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Delete_ShouldDeleteAddressByValidId()
        {
            var client = CreateHttpClient();
            var addressId = await GetFirstAddressId();

            var response = await client.DeleteAsync($"/api/Address?id={addressId}");

            response.EnsureSuccessStatusCode();
        }

        #region Helper methdos

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