using AutoMapper;
using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Features.Products.Commands;
using Inventory.Management.System.Logic.Features.Products.Database;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.Products.EventHandler
{
    public class ProductUpdateHandler : IRequestHandler<ProductUpdateCommand, OneOf<Task, NotFound>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductUpdateHandler> _logger;
        public ProductUpdateHandler(IUnitOfWork uow, IProductRepository productRepository, IMapper mapper, ILogger<ProductUpdateHandler> logger)
        {
            _uow = uow;
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<OneOf<Task, NotFound>> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _productRepository.GetProductById(request.Id);

                _mapper.Map(request, entity);
                entity.DateModified = DateTime.UtcNow;
                entity.DateCreated = DateTime.UtcNow;
                await _productRepository.Update(entity.Id, entity, cancellationToken);
                await _uow.SaveChanges(cancellationToken);

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product: {ProductName}", request.Name);
                throw;
            }
        }
    }
}
