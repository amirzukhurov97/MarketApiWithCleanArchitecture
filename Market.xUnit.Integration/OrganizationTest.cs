using Market.Application.DTOs.Address;
using Market.Application.DTOs.Organization;
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
    public class OrganizationTest : BaseTestEntity
    {
        [Fact]
        public async Task Create_ShouldCreateNewClient()
        {
            var client = CreateHttpClient();
            var newOrganization = new OrganizationRequest
            {
                Name = "Tests",
                AddressId = await GetFirstAddressId(),
                OrganizationTypeId = await GetFirstOrganizationTypeId(),
                PhoneNumber = "1234567890",
            };

            var response = await client.PostAsJsonAsync("/api/Organization", newOrganization);

            response.EnsureSuccessStatusCode();
            var createdOrganization = await response.Content.ReadAsStringAsync();
            Assert.NotNull(createdOrganization);
            Assert.Contains(newOrganization.Name, createdOrganization);
        }

        private async Task<Guid> GetFirstAddressId()
        {
            var client = CreateHttpClient();
            var response = await client.GetAsync("/api/Address");
            response.EnsureSuccessStatusCode();
            var addresses = await response.Content.ReadFromJsonAsync<IEnumerable<Address>>();
            return addresses!.First().Id;
        }

        private async Task<Guid> GetFirstOrganizationTypeId()
        {
            var client = CreateHttpClient();
            var response = await client.GetAsync("/api/OrganizationType");
            response.EnsureSuccessStatusCode();
            var organizationType = await response.Content.ReadFromJsonAsync<IEnumerable<OrganizationType>>();
            return organizationType!.First().Id;
        }
    }
}
