using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Products.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Management.System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        protected readonly IProductRepository _repository;
        protected readonly ILogger<ReportController> _logger;

        public ReportController(IProductRepository repository, ILogger<ReportController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet("low-stock-items")]
        public async Task<IActionResult> GetLowStockItems(CancellationToken token)
        {
            var lowStockProducts = await _repository.GetSet()
                .Where(p => p.StockQuantity < p.ReorderLevel)
                .OrderBy(p => p.StockQuantity)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.StockQuantity,
                    p.ReorderLevel
                })
                .ToListAsync(token);

            if (!lowStockProducts.Any())
                return NotFound(new { message = "No low-stock items found." });

            return Ok(lowStockProducts);
        }
        [HttpGet("inventory-valuation-summary")]
        public async Task<IActionResult> GetInventoryValuationSummary(CancellationToken token)
        {
            var inventorySummary = await _repository.GetSet()
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.StockQuantity,
                    p.UnitPrice,
                    TotalValue = p.StockQuantity * p.UnitPrice
                })
                .ToListAsync(token);

            if (!inventorySummary.Any())
                return NotFound(new { message = "No products found in inventory." });

            var totalInventoryValue = inventorySummary.Sum(p => p.TotalValue);

            return Ok(new
            {
                TotalInventoryValue = totalInventoryValue,
                Products = inventorySummary
            });
        }


    }
}
