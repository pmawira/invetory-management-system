using AutoMapper;
using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Categories.Database;
using Inventory.Management.System.Logic.Features.Products.Commands;
using Inventory.Management.System.Logic.Features.Products.Database;
using Inventory.Management.System.Logic.Features.Products.DTO;
using Inventory.Management.System.Logic.Features.StockAdditions.Database;
using Inventory.Management.System.Logic.Features.StockAdditions.DTO;
using Inventory.Management.System.Logic.Features.StockWithdrawals.Database;
using Inventory.Management.System.Logic.Features.StockWithdrawals.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Mime;
using System.Reflection;

namespace Inventory.Management.System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected readonly ILogger<ProductController> _logger;
        protected readonly IMapper _mapper;
        protected readonly ISender _sender;
        protected readonly IProductRepository _repository;
        protected readonly ICategoryRepository _categoryRepository;
        protected readonly IStockAdditionRepository _stockAdditionRepository;
        protected readonly IStockWithdrawalRepository _stockWithdrawalRepository;

        protected IUnitOfWork _uow;

        public ProductController(ILogger<ProductController> logger, ICategoryRepository categoryRepository, IMapper mapper, ISender sender, IProductRepository repository, IStockAdditionRepository stockAdditionRepository, IUnitOfWork uow, IStockWithdrawalRepository stockWithdrawalRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _sender = sender;
            _repository = repository;
            _categoryRepository = categoryRepository;
            _stockAdditionRepository = stockAdditionRepository;
            _uow = uow;
            _stockWithdrawalRepository = stockWithdrawalRepository;
        }
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(
        ProductCreateRequest request, CancellationToken token)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (!await _categoryRepository.ExistsByID(request.CategoryId))
                    return BadRequest(new { message = $"Category with ID {request.CategoryId} is not available" });

                var comamand = _mapper.Map<ProductCreateCommand>(request);
                var result = await _sender.Send(comamand);

                return CreatedAtAction(nameof(GetProduct), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product {name}", request.Name);
                return Problem(ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(EmptyResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProductUpdateRequest))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ConflictResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestResult))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(UnauthorizedResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id,
        ProductUpdateRequest request, CancellationToken token)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (!await _categoryRepository.ExistsByID(request.CategoryId))
                    return BadRequest(new { message = $"Category with ID {request.CategoryId} is not available" });

                var existingProduct = await _repository.GetById(id);
                if (existingProduct == null)
                    return NotFound(new { message = $"Product with ID {id} not found." });

                var command = _mapper.Map<ProductUpdateCommand>(request);
                command.Id = id;


                await _sender.Send(command, token);

                return Ok(new { message = "Product updated successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product {id}", id);
                return Problem(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repository.GetById((id));

            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet("List")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Product>))]
        public async Task<ActionResult<List<Product>>> List()
        {
            try
            {
                var products = _repository.GetSet().ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error listing banks");
                return Problem(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken token)
        {
            try
            {
                // Check if the product exists
                var product = await _repository.GetById(id);
                if (product == null)
                    return NotFound(new { message = $"Product with ID {id} not found." });

                _repository.Delete(product);
                return Ok(new { message = "Product deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product {id}", id);
                return Problem("An error occurred while deleting the product.");
            }
        }
        [HttpPost("add-stock")]
        public async Task<IActionResult> AddStock(StockAdditionRequest request, CancellationToken token)
        {
            try
            {
                var product = await _repository.GetById(request.ProductId);
                if (product == null)
                    return NotFound(new { message = $"Product with ID {request.ProductId} not found." });

                // Create new stock addition record
                var stockAddition = _mapper.Map<StockAddition>(request);
                await _stockAdditionRepository.Add(stockAddition);
                await _uow.SaveChanges(token);
                // Update product stock quantity
                product.StockQuantity += request.QuantityAdded;
                var command = _mapper.Map<ProductUpdateCommand>(product);
                await _sender.Send(command);

                return Ok(new { message = "Stock added successfully.", newStockLevel = product.StockQuantity });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding stock for product {id}", request.ProductId);
                return Problem("An error occurred while adding stock.");
            }
        }
        [HttpPost("withdraw-stock")]
        public async Task<IActionResult> WithdrawStock([FromBody] StockWithdrawalRequest request, CancellationToken token)
        {
            try
            {
                var product = await _repository.GetById(request.ProductId);
                if (product == null)
                    return NotFound(new { message = $"Product with ID {request.ProductId} not found." });

                // Prevent negative stock
                if (product.StockQuantity < request.QuantityWithdrawn)
                    return BadRequest(new { message = "Insufficient stock available." });

                // Record the withdrawal
                var stockWithdrawal = _mapper.Map<StockWithdrawal>(request);
               await _stockWithdrawalRepository.Add(stockWithdrawal);
               await _uow.SaveChanges(token);
                // Update product stock quantity
                product.StockQuantity -= request.QuantityWithdrawn;

                var command = _mapper.Map<ProductUpdateCommand>(product);
                await _sender.Send(command);

                return Ok(new { message = "Stock withdrawn successfully.", newStockLevel = product.StockQuantity });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error withdrawing stock for product {id}", request.ProductId);
                return Problem("An error occurred while withdrawing stock.");
            }
        }


    }
}
