using AutoMapper;
using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Products.Commands;
using Inventory.Management.System.Logic.Features.Products.Database;
using Inventory.Management.System.Logic.Features.Products.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ProductController(ILogger<ProductController> logger, IMapper mapper, ISender sender, IProductRepository repository)
        {
            _logger = logger; 
            _mapper = mapper;
            _sender = sender;
            _repository = repository;
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

                var comamand =  _mapper.Map<ProductCreateCommand>(request);
                var result = await _sender.Send(comamand);

                return CreatedAtAction(nameof(GetProduct), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product {name}", request.Name);
                return Problem(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repository.GetProductById((id));

            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}
