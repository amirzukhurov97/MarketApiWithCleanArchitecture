using Market.Application.DTOs.Address;
using Market.Application.Services;
using MarketApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MarketApi.Tests.Controllers
{
    [TestClass]
    public class AddressControllerTests
    {
        private Mock<IGenericService<AddressRequest, AddressUpdateRequest, AddressResponse>> _mockService;
        private Mock<ILogger<AddressController>> _mockLogger;
        private AddressController? _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IGenericService<AddressRequest, AddressUpdateRequest, AddressResponse>>();
            _mockLogger = new Mock<ILogger<AddressController>>();
            _controller = new AddressController(_mockService.Object, _mockLogger.Object);
        }

        [TestMethod]
        public void GetAll_ReturnsOk_WhenAddressesExist()
        {
            var mockAddresses = new List<AddressResponse>
            {
                new AddressResponse { Id = Guid.NewGuid(), Name = "Test Address" }
            };
            _mockService.Setup(s => s.GetAll()).Returns(mockAddresses);

            var result = _controller.GetAll();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var addresses = okResult.Value as IEnumerable<AddressResponse>;
            Assert.IsNotNull(addresses);
        }
        [TestMethod]
        public void GetAll_ReturnsNotFound_WhenNoAddressesExist()
        {
            _mockService.Setup(s => s.GetAll()).Returns(new List<AddressResponse>());

            var result = _controller.GetAll();

            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("No addresses found.", notFoundResult.Value);
        }

        [TestMethod]
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

        [TestMethod]
        public void Create_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var request = new AddressRequest(); // пустое имя
            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = _controller.Create(request);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [TestMethod]
        public async Task Update_ReturnsOk_WhenModelIsValid()
        {
            // Arrange
            var request = new AddressUpdateRequest { Name = "Valid Address" };
            var expectedMessage = "Created new item with this ID: 123";
            _mockService.Setup(s => s.Update(request)).Returns(expectedMessage);

            // Act
            var result = _controller.Put(request);

            // Assert
            var actionResult = result.Result as OkObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(200, actionResult.StatusCode);
            Assert.AreEqual(expectedMessage, actionResult.Value);
        }

        [TestMethod]
        public void Update_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var request = new AddressUpdateRequest(); // пустое имя
            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = _controller.Put(request);

            // Assert
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
        [TestMethod]
        public void Delete_ReturnsOk_WhenAddressIsDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedMessage = "Address is deleted";
            _mockService.Setup(s => s.Remove(id)).Returns(expectedMessage);

            // Act
            var result = _controller.Delete(id);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(expectedMessage, okResult.Value);
        }

        [TestMethod]
        public void Delete_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var id = Guid.NewGuid();
            string expectedMessage = null;
            _mockService.Setup(s => s.Remove(id)).Returns(expectedMessage);

            // Act
            var result = _controller.Delete(id);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual(expectedMessage, notFoundResult.Value);
        }
    }


}
