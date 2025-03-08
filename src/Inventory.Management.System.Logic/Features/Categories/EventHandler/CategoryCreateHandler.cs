using AutoMapper;
using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Categories.Commands;
using Inventory.Management.System.Logic.Features.Categories.Database;
using Inventory.Management.System.Logic.Features.Categories.DTO;
using Inventory.Management.System.Logic.Features.Products.Commands;
using Inventory.Management.System.Logic.Features.Products.Database;
using Inventory.Management.System.Logic.Features.Products.DTO;
using Inventory.Management.System.Logic.Features.Products.EventHandler;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.Categories.EventHandler
{
    public class CategoryCreateHandler: IRequestHandler<CategoryCreateCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryCreateHandler> _logger;
        public CategoryCreateHandler(IUnitOfWork uow, ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoryCreateHandler> logger)
        {
            _uow = uow;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Map the request DTO to Product entity
                var item = _mapper.Map<Category>(request);
                item.DateCreated = DateTime.UtcNow;
                _logger.LogInformation("Creating {item} to the db", item);
                // Add the new product
                await _categoryRepository.Add(item);
                await _uow.SaveChanges(cancellationToken);
              
            }
            catch (Exception ex)
            {
                // Log the error properly
                _logger.LogError(ex, "Error creating product: {categoryName}", request.Name);
                throw;
            }
        }
    }
}
