using Market.Application.DTOs.Address;
using Market.Application.Services;
using MarketApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Market.Tests
{
    public class AddressControllerTests
    {
        private readonly Mock<IGenericService<AddressRequest, AddressUpdateRequest, AddressResponse>> _mockService;
        private readonly Mock<ILogger<AddressController>> _mockLogger;
        private readonly AddressController _controller;

        public AddressControllerTests()
        {
            _mockService = new Mock<IGenericService<AddressRequest, AddressUpdateRequest, AddressResponse>>();
            _mockLogger = new Mock<ILogger<AddressController>>();
            _controller = new AddressController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetAll_ReturnsOk_WhenAddressesExist()
        {
            // Arrange
            var mockData = new List<AddressResponse>
            {
                new AddressResponse { Id = Guid.NewGuid(), Name = "Test Address" }
            };
            _mockService.Setup(s => s.GetAll()).Returns(mockData);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var addresses = Assert.IsAssignableFrom<IEnumerable<AddressResponse>>(okResult.Value);
            Assert.Single(addresses);
        }

        [Fact]
        public void GetAll_ReturnsNotFound_WhenNoAddressesExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetAll()).Returns(Enumerable.Empty<AddressResponse>());

            // Act
            var result = _controller.GetAll();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void GetById_ReturnsOk_WhenAddressExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var response = new AddressResponse { Id = id, Name = "Test Address" };
            _mockService.Setup(s => s.GetById(id)).Returns(response);

            // Act
            var result = _controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var address = Assert.IsType<AddressResponse>(okResult.Value);
            Assert.Equal(id, address.Id);
        }

        [Fact]
        public void GetById_ReturnsNotFound_WhenAddressDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetById(id)).Returns((AddressResponse)null);

            // Act
            var result = _controller.GetById(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Create_ReturnsOk_WhenModelIsValid()
        {
            // Arrange
            var request = new AddressRequest { Name = "New Address" };
            var expectedResponse = $"Created new item with this ID: {Guid.NewGuid()}";
            _mockService.Setup(s => s.Create(request)).Returns(expectedResponse);

            // Act
            var result = _controller.Create(request);

            // Assert
            var okResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal(expectedResponse, okResult.Value);
        }

    }
}
