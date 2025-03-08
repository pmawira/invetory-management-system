using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Inventory.Management.System.Logic.Database.Models;

namespace Inventory.Management.System.Logic.Tests
{
    public class ProductApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProductApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task AddProduct_ShouldReturn_CreatedStatus()
        {
            // Arrange
            var newProduct = new
            {
                Name = "Test Product",
                Description = "A sample test product",
                CategoryId = 1,
                UnitPrice = 500,
                ReorderLevel = 10
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/products", newProduct);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetLowStockItems_ShouldReturn_ProductsBelowReorderLevel()
        {
            // Act
            var response = await _client.GetAsync("/api/products/low-stock");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            products.Should().NotBeEmpty();
            products.Should().OnlyContain(p => p.StockQuantity < p.ReorderLevel);
        }
    }
}
