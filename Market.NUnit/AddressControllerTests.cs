using Market.Application.DTOs.Address;
using Market.Application.Services;
using Market.Application.SeviceInterfacies;
using MarketApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MarketApi.Tests.Controllers
{
    [TestFixture]
    public class AddressControllerTests
    {
        private Mock<IAddressService<AddressRequest, AddressUpdateRequest, AddressResponse>> _mockService;
        private Mock<ILogger<AddressController>> _mockLogger;
        private AddressController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IAddressService<AddressRequest, AddressUpdateRequest, AddressResponse>>();
            _mockLogger = new Mock<ILogger<AddressController>>();
            _controller = new AddressController(_mockService.Object, _mockLogger.Object);
        }

        [Test]
        public void GetAll_ReturnsOk_WhenDataExists()
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
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetAll_ReturnsNotFound_WhenNoData()
        {
            // Arrange
            _mockService.Setup(s => s.GetAll()).Returns(new List<AddressResponse>());

            // Act
            var result = _controller.GetAll();

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public void GetById_ReturnsOk_WhenAddressExists()
        {
            var id = Guid.NewGuid();
            var response = new AddressResponse { Id = id, Name = "Test Address" };
            _mockService.Setup(s => s.GetById(id)).Returns(response);

            var result = _controller.GetById(id);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(response, okResult.Value);
        }

        [Test]
        public void GetById_ReturnsNotFound_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.GetById(id)).Returns((AddressResponse)null);

            var result = _controller.GetById(id);

            var notFound = result as NotFoundObjectResult;
            Assert.IsNotNull(notFound);
            Assert.AreEqual(404, notFound.StatusCode);
        }

        [Test]
        public void Create_ReturnsOk_WhenModelIsValid()
        {
            var request = new AddressRequest { Name = "Valid Address" };
            var expectedMessage = "Created new item with this ID: 123";
            _mockService.Setup(s => s.Create(request)).Returns(expectedMessage);

            var result = _controller.Create(request);

            var actionResult = result.Result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(200, actionResult.StatusCode);
            Assert.AreEqual(expectedMessage, actionResult.Value);
        }

        [Test]
        public void Create_ReturnsBadRequest_WhenModelStateInvalid()
        {
            var request = new AddressRequest();
            _controller.ModelState.AddModelError("Name", "Name is required");

            var result = _controller.Create(request);

            var badRequest = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(400, badRequest.StatusCode);
        }

        [Test]
        public void Delete_ReturnsOk_WhenDeleted()
        {
            var id = Guid.NewGuid();
            var message = "Address is deleted";
            _mockService.Setup(s => s.Remove(id)).Returns(message);

            var result = _controller.Delete(id);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(message, okResult.Value);
        }

        [Test]
        public void Delete_ThrowsException_WhenErrorOccurs()
        {
            var id = Guid.NewGuid();
            _mockService.Setup(s => s.Remove(id)).Throws(new Exception("Test exception"));

            var ex = Assert.Throws<Exception>(() => _controller.Delete(id));
            Assert.That(ex.Message, Is.EqualTo("Test exception"));
        }
    }
}
