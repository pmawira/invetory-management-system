using AutoMapper;
using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Products.Commands;
using Inventory.Management.System.Logic.Features.Products.Database;
using Inventory.Management.System.Logic.Features.Products.DTO;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.Products.EventHandler
{
    public class ProductCreateHandler : IRequestHandler<ProductCreateCommand, ProductDTO>
    {
        private readonly IUnitOfWork _uow;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductCreateHandler> _logger;
        public ProductCreateHandler(IUnitOfWork uow, IProductRepository productRepository, IMapper mapper, ILogger<ProductCreateHandler> logger)
        {
            _uow = uow;
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ProductDTO> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Map the request DTO to Product entity
                var item = _mapper.Map<Product>(request);

                // Add the new product
                await _productRepository.Add(item);
                await _uow.SaveChanges(cancellationToken);

                // Retrieve the saved product from the database
                var product = await _productRepository.GetProductByName(item.Name);

                // Map to DTO
                var dto = _mapper.Map<ProductDTO>(product);

                return dto;
            }
            catch (Exception ex)
            {
                // Log the error properly
                _logger.LogError(ex, "Error creating product: {ProductName}", request.Name);
                throw;
            }
        }
    }
}
