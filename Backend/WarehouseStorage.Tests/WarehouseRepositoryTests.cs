using Bogus;
using Microsoft.EntityFrameworkCore;
using WarehouseStorage.Domain.DomainPrimitives;
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
        public async Task AddWarehouse_Should_Save_And_Retrieve_Warehouse()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
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
            using var context = CreateInMemoryDbContext();
            var repo = new WarehouseRepository(context);
            var warehouse = GenerateFakeWarehouse();
            await repo.Add(warehouse);

            // Act
            var newCity = new City("Updated City");
            // Update the tracked Address instance in-place to avoid EF Core tracking conflicts.
            var address = warehouse.Location.Address;
            var cityProperty = typeof(Address).GetProperty("City", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            if (cityProperty == null)
            {
                throw new InvalidOperationException("Cannot find City property on Address.");
            }
            cityProperty.SetValue(address, newCity);

            await repo.Update(warehouse);
            var updated = await repo.GetById(warehouse.Id.Value);

            // Assert
            Assert.NotNull(updated);
            Assert.Equal("Updated City", updated.Location.Address.City.value);
        }


        [Fact]
        public async Task GetAllWarehouses_Should_Return_Correct_Number_Of_Warehouses()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
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
            using var context = CreateInMemoryDbContext();
            var repo = new WarehouseRepository(context);
            var warehouse = GenerateFakeWarehouse();

            // Act
            await repo.Add(warehouse);
            await repo.Delete(warehouse.Id.Value);
            var retrieved = await repo.GetById(warehouse.Id.Value);

            // Assert
            Assert.Null(retrieved);
        }

        [Fact]
        public async Task Add_NullWarehouse_ThrowsArgumentNullException()
        {
            using var context = CreateInMemoryDbContext();
            var repo = new WarehouseRepository(context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => repo.Add(null!));
        }

        [Fact]
        public async Task Update_NullWarehouse_ThrowsArgumentNullException()
        {
            using var context = CreateInMemoryDbContext();
            var repo = new WarehouseRepository(context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => repo.Update(null!));
        }

        [Fact]
        public async Task Update_EmptyId_ThrowsArgumentException()
        {
            using var context = CreateInMemoryDbContext();
            var repo = new WarehouseRepository(context);
            var warehouse = GenerateFakeWarehouse();
            var warehouseWithEmptyId = new Warehouse(Guid.Empty) { Location = warehouse.Location };

            await Assert.ThrowsAsync<ArgumentException>(() => repo.Update(warehouseWithEmptyId));
        }

        [Fact]
        public async Task Delete_NonExistingWarehouse_ThrowsKeyNotFoundException()
        {
            using var context = CreateInMemoryDbContext();
            var repo = new WarehouseRepository(context);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => repo.Delete(Guid.NewGuid()));
        }

    }
}
        