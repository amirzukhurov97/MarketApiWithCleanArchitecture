using Market.Application.DTOs.OrganizationType;
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
    public class OrganizationTypeTest : BaseTestEntity
    {
        [Fact]
        public async Task Create_ShouldCreateNewOrganizationType()
        {
            var client = CreateHttpClient();
            var newOrganizationType = new OrganizationTypeRequest
            {
                Name = "TestOrganizationType"
            };

            var response = await client.PostAsJsonAsync("/api/OrganizationType", newOrganizationType);

            response.EnsureSuccessStatusCode();
            var createdOrganizationType = await response.Content.ReadAsStringAsync();
            Assert.NotNull(createdOrganizationType);
            Assert.Contains(newOrganizationType.Name, createdOrganizationType);
        }
    }
}
