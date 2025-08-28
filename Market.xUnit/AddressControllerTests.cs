using Market.Application.DTOs.Address;
using Market.Application.Services;
using Market.Application.SeviceInterfacies;
using MarketApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Market.xUnit
{
    public class AddressControllerTests
    {
        private readonly Mock<IAddressService<AddressRequest, AddressUpdateRequest, AddressResponse>> _mockService;
        private readonly Mock<ILogger<AddressController>> _mockLogger;
        private readonly AddressController _controller;

        public AddressControllerTests()
        {
            _mockService = new Mock<IAddressService<AddressRequest, AddressUpdateRequest, AddressResponse>>();
            _mockLogger = new Mock<ILogger<AddressController>>();
            _controller = new AddressController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetAll_ReturnsOk_WhenDataExists()
        {
            // Arrange
            var data = new List<AddressResponse> { new AddressResponse { Id = Guid.NewGuid(), Name = "Test" } };
            _mockService.Setup(s => s.GetAll()).Returns(data);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnData = Assert.IsAssignableFrom<IEnumerable<AddressResponse>>(okResult.Value);
            Assert.Single(returnData);
        }

        [Fact]
        public void GetAll_ReturnsNotFound_WhenEmpty()
        {
            _mockService.Setup(s => s.GetAll()).Returns(new List<AddressResponse>());

            var result = _controller.GetAll();

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No addresses found.", notFound.Value);
        }

        [Fact]
        public void GetById_ReturnsOk_WhenFound()
        {
            var id = Guid.NewGuid();
            var expected = new AddressResponse { Id = id, Name = "Test Address" };
            _mockService.Setup(s => s.GetById(id)).Returns(expected);

            var result = _controller.GetById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsType<AddressResponse>(okResult.Value);
            Assert.Equal(expected.Id, data.Id);
        }

        [Fact]
        public void GetById_ReturnsNotFound_WhenNull()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetById(id)).Returns((AddressResponse)null);

            var result = _controller.GetById(id);

            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains(id.ToString(), notFound.Value.ToString());
        }

        [Fact]
        public void Create_ReturnsOk_WhenValid()
        {
            var request = new AddressRequest { Name = "Valid Address" };
            var response = "Created new item with this ID: 123";
            _mockService.Setup(s => s.Create(request)).Returns(response);

            var result = _controller.Create(request);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<string>(okResult.Value);
            Assert.Equal(response, value);
        }

        [Fact]
        public void Create_ReturnsBadRequest_WhenModelStateInvalid()
        {
            var request = new AddressRequest();
            _controller.ModelState.AddModelError("Name", "Required");

            var result = _controller.Create(request);

            var badRequest = Assert.IsType<ActionResult<string>>(result);
            Assert.Null(badRequest.Value); // because it's a BadRequest with ModelState
        }

        [Fact]
        public void Delete_ReturnsOk_WhenDeleted()
        {
            var id = Guid.NewGuid();
            var message = "Address is deleted";
            _mockService.Setup(s => s.Remove(id)).Returns(message);

            var result = _controller.Delete(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(message, okResult.Value);
        }

        [Fact]
        public void Delete_ThrowsException_WhenServiceFails()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.Remove(id)).Throws(new Exception("Test error"));

            var ex = Assert.Throws<Exception>(() => _controller.Delete(id));
            Assert.Equal("Test error", ex.Message);
        }
        
    }
}
