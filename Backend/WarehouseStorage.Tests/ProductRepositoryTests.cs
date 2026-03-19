
using Bogus;
using Microsoft.EntityFrameworkCore;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.Domain.DomainPrimitives;
using WarehouseStorage.Services.Repositories.Repositories;
using Xunit;

namespace WarehouseStorage.Tests
{
    public class ProductRepositoryTests
    {
        private static WarehouseDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new WarehouseDbContext(options);
        }

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
        public async Task AddProduct_Should_Save_And_Retrieve_Product()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new ProductRepository(context);
            var product = GenerateFakeProduct();

            // Act
            await repo.Add(product);
            var retrieved = await repo.GetById(product.Id.Value);

            // Assert
            Assert.NotNull(retrieved);
            Assert.Equal(product.Id, retrieved.Id);
            Assert.Equal(product.Name.value, retrieved.Name.value);
            Assert.Equal(product.Number.value, retrieved.Number.value);
            Assert.Equal(product.DefaultPrice.value, retrieved.DefaultPrice.value);
            Assert.Equal(product.DefaultCurrency.value, retrieved.DefaultCurrency.value);
        }

        [Fact]
        public async Task UpdateProduct_Should_Modify_Existing_Product()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new ProductRepository(context);
            var product = GenerateFakeProduct();
            await repo.Add(product);

            // Act
            product.Update(new ProductName("Updated Name"), product.Number, product.DefaultPrice, product.DefaultCurrency);
            await repo.Update(product);
            var updated = await repo.GetById(product.Id.Value);

            // Assert
            Assert.NotNull(updated);
            Assert.Equal("Updated Name", updated.Name.value);
        }


        [Fact]
        public async Task GetAll_Should_Return_All_Products()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new ProductRepository(context);
            var products = Enumerable.Range(0, 5).Select(_ => GenerateFakeProduct()).ToArray();
            foreach (var product in products)
            {
                await repo.Add(product);
            }

            // Act
            var retrieved = await repo.GetAll();

            // Assert
            Assert.Equal(5, retrieved.Length);
        }

        [Fact]
        public async Task Delete_Should_Remove_Product()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();
            var repo = new ProductRepository(context);
            var product = GenerateFakeProduct();
            await repo.Add(product);

            // Act
            await repo.Delete(product.Id.Value);
            var deleted = await repo.GetById(product.Id.Value);

            // Assert
            Assert.Null(deleted);
        }

        [Fact]
        public async Task Add_NullProduct_ThrowsArgumentNullException()
        {
            using var context = CreateInMemoryDbContext();
            var repo = new ProductRepository(context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => repo.Add(null!));
        }

        [Fact]
        public async Task Update_NullProduct_ThrowsArgumentNullException()
        {
            using var context = CreateInMemoryDbContext();
            var repo = new ProductRepository(context);

            await Assert.ThrowsAsync<ArgumentNullException>(() => repo.Update(null!));
        }

        [Fact]
        public async Task Update_EmptyId_ThrowsArgumentException()
        {
            using var context = CreateInMemoryDbContext();
            var repo = new ProductRepository(context);
            var product = GenerateFakeProduct();
            var productWithEmptyId = new Product(product.Name, product.Number, product.DefaultPrice, product.DefaultCurrency, Guid.Empty);

            await Assert.ThrowsAsync<ArgumentException>(() => repo.Update(productWithEmptyId));
        }

        [Fact]
        public async Task Delete_NonExistingProduct_DoesNotThrow()
        {
            using var context = CreateInMemoryDbContext();
            var repo = new ProductRepository(context);

            await repo.Delete(Guid.NewGuid());
        }
    }
}
