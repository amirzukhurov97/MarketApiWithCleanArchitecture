using Market.Application.DTOs.Address;
using Market.Application.DTOs.Measurement;
using Market.xUnit.Integration.Configurations;
using System.Net.Http.Json;

namespace Market.xUnit.Integration
{
    public class MeasurementTest : BaseTestEntity
    {
        [Fact]
        public async Task Create_ShouldCreateNewClient()
        {
            var client = CreateHttpClient();
            var newAddress = new MeasurementRequest
            {
                Name = "TestsMeasurement"
            };

            var response = await client.PostAsJsonAsync("/api/Measurement", newAddress);

            response.EnsureSuccessStatusCode();
            var createdMeasurement = await response.Content.ReadAsStringAsync();
            Assert.NotNull(createdMeasurement);
            Assert.Contains(newAddress.Name, createdMeasurement);
        }
    }
}
