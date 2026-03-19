using Microsoft.AspNetCore.Mvc;
using WarehouseStorage.Services.Repositories.Repositories;
using WarehouseStorage.Api.Controllers;
using Microsoft.EntityFrameworkCore;
using WarehouseStorage.Services.Factories;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.Domain.DomainPrimitives;
using Bogus;

namespace WarehouseStorage.Tests
{ 
    public class ProductControllerTests
    {

        private static Product GenerateFakeProduct()
        {
            var faker = new Faker();
            return new Product(
                new ProductName(faker.Commerce.ProductName()),
                new ProductNumber(faker.Random.AlphaNumeric(12)),
                // Use an integer price to satisfy the domain's 2-decimal-place validation
                new Price(faker.Random.Int(1, 9999)),
                new Currency("USD"),
                Guid.NewGuid()
            );
        }

        [Fact]
        public async Task Add_ValidProduct_ReturnsCreated()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Add_ValidProduct_ReturnsCreated")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new ProductRepository(context);
            var controller = new ProductController(repo);

            var product = GenerateFakeProduct();

            // Act
            var result = await controller.Add(ModelFactory.CreateProductDTO(product)) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
        }


        [Fact]
        public async Task Read_ExistingProduct_ReturnsOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Read_ExistingProduct_ReturnsOk")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new ProductRepository(context);
            var controller = new ProductController(repo);

            var product = GenerateFakeProduct();
            await repo.Add(product);

            // Act
            var result = await controller.Read(product.Id.Value) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async Task Read_NonExistingProduct_ReturnsNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Read_NonExistingProduct_ReturnsNotFound")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new ProductRepository(context);
            var controller = new ProductController(repo);

            // Act
            var result = await controller.Read(Guid.NewGuid()) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }


        [Fact]
        public async Task ReadAll_ValidRequest_ReturnsOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_ReadAll_ValidRequest_ReturnsOk")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new ProductRepository(context);
            var controller = new ProductController(repo);

            for (int i = 0; i < 5; i++)
            {
                await repo.Add(GenerateFakeProduct());
            }

            // Act
            var result = await controller.ReadAll() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async Task ReadAll_InvalidTake_ReturnsBadRequest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_ReadAll_InvalidTake_ReturnsBadRequest")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new ProductRepository(context);
            var controller = new ProductController(repo);

            // Act
            var result = await controller.ReadAll(take: 0) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }


        [Fact]
        public async Task ReadAll_TooManyTake_ReturnsBadRequest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_ReadAll_TooManyTake_ReturnsBadRequest")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new ProductRepository(context);
            var controller = new ProductController(repo);

            // Act
            var result = await controller.ReadAll(take: 1001) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }


        [Fact]
        public async Task Delete_ExistingProduct_ReturnsOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Delete_ExistingProduct_ReturnsOk")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new ProductRepository(context);
            var controller = new ProductController(repo);

            var product = GenerateFakeProduct();
            await repo.Add(product);

            // Act
            var result = await controller.Delete(product.Id.Value) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }


        [Fact]
        public async Task Delete_NonExistingProduct_ReturnsNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Delete_NonExistingProduct_ReturnsNotFound")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new ProductRepository(context);
            var controller = new ProductController(repo);

            // Act
            var result = await controller.Delete(Guid.NewGuid()) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }
    }
}