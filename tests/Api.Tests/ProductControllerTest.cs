using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductController _controller;

        public ProductControllerTest()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product1" },
                new Product { Id = Guid.NewGuid(), Name = "Product2" },
            };
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.Equal(products, result);
        }

        [Fact]
        public async Task GetById_ReturnsProduct_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "Product1" };
            _mockService.Setup(s => s.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(product, actionResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockService.Setup(s => s.GetByIdAsync(productId)).ReturnsAsync((Product?)null);

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedProduct()
        {
            // Arrange
            var product = new Product { Id = Guid.NewGuid(), Name = "Product1" };
            _mockService.Setup(s => s.AddAsync(product)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(product);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(product, actionResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "UpdatedProduct" };
            _mockService.Setup(s => s.UpdateAsync(product)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(productId.ToString(), product);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = Guid.NewGuid(), Name = "UpdatedProduct" };

            // Act
            var result = await _controller.Update(productId.ToString(), product);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID mismatch", badRequestResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockService.Setup(s => s.DeleteAsync(productId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(productId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
