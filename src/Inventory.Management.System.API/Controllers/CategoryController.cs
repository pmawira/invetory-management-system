using AutoMapper;
using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Categories.Commands;
using Inventory.Management.System.Logic.Features.Categories.Database;
using Inventory.Management.System.Logic.Features.Categories.DTO;
using Inventory.Management.System.Logic.Features.Products.Commands;
using Inventory.Management.System.Logic.Features.Products.Database;
using Inventory.Management.System.Logic.Features.Products.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Inventory.Management.System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        protected readonly ILogger<CategoryController> _logger;
        protected readonly IMapper _mapper;
        protected readonly ISender _sender;
        protected readonly ICategoryRepository _repository;
        public CategoryController(ILogger<CategoryController> logger, IMapper mapper, ISender sender, ICategoryRepository repository)
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
       CategoryCreateRequest request, CancellationToken token)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var comamand = _mapper.Map<CategoryCreateCommand>(request);
                await _sender.Send(comamand, token);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product {name}", request.Name);
                return Problem(ex.Message);
            }
        }
        [HttpGet("List")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Category>))]
        public async Task<ActionResult<List<Category>>> List()
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetCategory(int id)
        {
            var product = await _repository.GetById((id));

            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}
