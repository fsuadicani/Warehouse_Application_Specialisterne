using Bogus;
using Microsoft.EntityFrameworkCore;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.Services.Repositories.Repositories;

namespace WarehouseStorage.Tests
{
    public class WarehouseRepositoryTests
    {
        private static WarehouseDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new WarehouseDbContext(options);
        }

        private static Warehouse GenerateFakeWarehouse()
        {
            var faker = new Faker();
            return new Warehouse(  
                Guid.NewGuid()
            );
        }


        [Fact]
        public async Task AddWarehouse_Should_Save_And_Retrieve_Warehouse()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            var repo = new WarehouseRepository(context);
            var warehouse = GenerateFakeWarehouse();

            // Act
            await repo.Add(warehouse);
            var retrieved = await repo.GetById(warehouse.Id.Value);

            // Assert
            Assert.NotNull(retrieved);
            Assert.Equal(warehouse.Location, retrieved.Location);
        }


        [Fact]
        public async Task UpdateWarehouse_Should_Modify_Existing_Warehouse()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            var repo = new WarehouseRepository(context);
            var warehouse = GenerateFakeWarehouse();
        }


        [Fact]
        public async Task GetAllWarehouses_Should_Return_Correct_Number_Of_Warehouses()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            var repo = new WarehouseRepository(context);
            for (int i = 0; i < 5; i++)
            {
                await repo.Add(GenerateFakeWarehouse());
            }

            // Act
            var warehouses = await repo.GetAll();

            // Assert
            Assert.Equal(5, warehouses.Length);
        }


        [Fact]
        public async Task DeleteWarehouse_Should_Remove_Warehouse()
        {
            // Arrange
            var context = CreateInMemoryDbContext();
            var repo = new WarehouseRepository(context);
            var warehouse = GenerateFakeWarehouse();

            // Act
            await repo.Add(warehouse);
            await repo.Delete(warehouse.Id.Value);
            var retrieved = await repo.GetById(warehouse.Id.Value);

            // Assert
            Assert.Null(retrieved);
        }

    }
}
        