using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Database;
using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Products.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Tests
{
    public class ProductRepositoryTests
    {
        private readonly ProductRepository _repository;
        private readonly InventoryManagementSystemContext _context;
        public ProductRepositoryTests()
        {
            // Use In-Memory Database for Testing
            var options = new DbContextOptionsBuilder<InventoryManagementSystemContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new InventoryManagementSystemContext(options);
            // Mock ILogger<UnitOfWork> to prevent the error
            var mockLogger = new Mock<ILogger<UnitOfWork>>();

            // Seed test data
            SeedTestData();

            // Initialize Repository
            var unitOfWork = new UnitOfWork(_context, mockLogger.Object);
            _repository = new ProductRepository(unitOfWork);
        }

        private void SeedTestData()
        {
            if (!_context.Products.Any()) // Prevent duplicate entries
            {
                _context.Products.AddRange(
                    new Product { Name = "Laptop", Description = "Gaming Laptop", CategoryId = 1, UnitPrice = 1500, ReorderLevel = 5 },
                    new Product { Name = "Keyboard", Description = "Mechanical Keyboard", CategoryId = 1, UnitPrice = 100, ReorderLevel = 10 }
                );
                _context.SaveChanges();
            }

        }

        [Fact]
        public async Task GetById_ShouldReturnCorrectProduct()
        {
            // Act
            var product = await _repository.GetById(1);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(1, product.Id);
            Assert.Equal("Laptop", product.Name);
        }
        [Fact]
        public async Task GetById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Act
            var product = await _repository.GetById(99); // Non-existent product

            // Assert
            Assert.Null(product);
        }

        [Fact]
        public async Task GetByName_ShouldReturnProduct_WhenProductExists()
        {
            // Act
            var product = await _repository.GetByName("Keyboard");

            // Assert
            Assert.NotNull(product);
            Assert.Equal("Keyboard", product.Name);
        }

        [Fact]
        public async Task GetByName_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Act
            var product = await _repository.GetByName("Mouse");

            // Assert
            Assert.Null(product);
        }

        [Fact]
        public async Task DeleteProduct_ShouldRemoveProduct()
        {
            // Arrange
            var product = await _repository.GetById(2);
            Assert.NotNull(product);

            // Act
            _repository.Delete(product);
            await _context.SaveChangesAsync();
            // Save changes

            // Assert
            var deletedProduct = await _repository.GetById(2);
            Assert.Null(deletedProduct);
        }
    }
}
