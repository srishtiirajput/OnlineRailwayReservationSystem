using Moq;
using RailwayReservation.Controllers;
using RailwayReservation.Interfaces;
using RailwayReservation.Models;
using RailwayReservation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace SupportTesting
{
    [TestClass]
    public class SupportControllerTests
    {
        [TestMethod]
        public async Task CreateSupport_ValidData_ReturnsOk()
        {
            // Arrange
            var mockRepo = new Mock<ISupport>();
            var controller = new SupportController(mockRepo.Object);
            var request = new SupportRequest { UserQuery = "Help me!", UserId = "user123" };

            // Mock the CreateSupportAsync method to return a dummy support ticket
            mockRepo.Setup(repo => repo.CreateSupportAsync(request.UserQuery, request.UserId))
                    .ReturnsAsync(new Support { Query = request.UserQuery, UserId = request.UserId });

            // Act
            var result = await controller.CreateSupport(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var support = okResult.Value as Support;
            Assert.AreEqual("Help me!", support.Query);
        }

        [TestMethod]
        public async Task CreateSupport_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<ISupport>();
            var controller = new SupportController(mockRepo.Object);
            var request = new SupportRequest { UserQuery = "", UserId = "user123" };

            // Act
            var result = await controller.CreateSupport(request);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Query and UserId cannot be empty.", badRequestResult.Value);
        }
    }
}
