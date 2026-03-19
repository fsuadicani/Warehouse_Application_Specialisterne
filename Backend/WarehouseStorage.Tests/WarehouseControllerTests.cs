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
    public class WarehouseControllerTests
    {
        private static Warehouse GenerateFakeWarehouse()
        {
            var faker = new Faker();
            var address = new Address(
                new City(faker.Address.City()),
                new StreetName(faker.Address.StreetName()),
                new StreetNumber(faker.Address.BuildingNumber()),
                new ZipCode(faker.Address.ZipCode().Replace("-", "")),
                Guid.NewGuid()
            );
            var location = new Location
            {
                Address = address,
                Stocks = new List<Stock?>()
            };
            var warehouse = new Warehouse(Guid.NewGuid())
            {
                Location = location
            };
            return warehouse;
        }


        [Fact]
        public async Task Add_ValidWarehouse_ReturnsCreated()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Add_ValidWarehouse_ReturnsCreated")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new WarehouseRepository(context);
            var controller = new WarehouseController(repo);

            var warehouse = GenerateFakeWarehouse();

            // Act
            var result = await controller.Add(ModelFactory.CreateWarehouseDTO(warehouse)) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
        }


        [Fact]
        public async Task Read_ExistingWarehouse_ReturnsOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Read_ExistingWarehouse_ReturnsOk")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new WarehouseRepository(context);
            var controller = new WarehouseController(repo);

            var warehouse = GenerateFakeWarehouse();
            await repo.Add(warehouse);

            // Act
            var result = await controller.Read(warehouse.Id.Value) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async Task Read_NonExistingWarehouse_ReturnsNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Read_NonExistingWarehouse_ReturnsNotFound")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new WarehouseRepository(context);
            var controller = new WarehouseController(repo);

            // Act
            var result = await controller.Read(Guid.NewGuid()) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }


        [Fact]
        public async Task Update_ExistingWarehouse_ReturnsOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Update_ExistingWarehouse_ReturnsOk")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new WarehouseRepository(context);
            var controller = new WarehouseController(repo);

            var warehouse = GenerateFakeWarehouse();
            await repo.Add(warehouse);

            // Act
            var newCity = new City("Updated City");
            warehouse.Location.Address = new Address(
                newCity,
                warehouse.Location.Address.Street,
                warehouse.Location.Address.StreetNumber,
                warehouse.Location.Address.ZipCode,
                warehouse.Location.Address.Id
            );
            var result = await controller.Update(warehouse.Id.Value, ModelFactory.CreateWarehouseDTO(warehouse)) as OkResult;
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async Task Update_NonExistingWarehouse_ReturnsNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Update_NonExistingWarehouse_ReturnsNotFound")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new WarehouseRepository(context);
            var controller = new WarehouseController(repo);

            var warehouse = GenerateFakeWarehouse();

            // Act
            var result = await controller.Update(Guid.NewGuid(), ModelFactory.CreateWarehouseDTO(warehouse)) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }


        [Fact]
        public async Task Delete_ExistingWarehouse_ReturnsOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Delete_ExistingWarehouse_ReturnsOk")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new WarehouseRepository(context);
            var controller = new WarehouseController(repo);

            var warehouse = GenerateFakeWarehouse();
            await repo.Add(warehouse);

            // Act
            var result = await controller.Delete(warehouse.Id.Value) as OkResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async Task Delete_NonExistingWarehouse_ReturnsNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Delete_NonExistingWarehouse_ReturnsNotFound")
                .Options;

            using var context = new WarehouseDbContext(options);
            var repo = new WarehouseRepository(context);
            var controller = new WarehouseController(repo);

            // Act
            var result = await controller.Delete(Guid.NewGuid()) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }
    }
}