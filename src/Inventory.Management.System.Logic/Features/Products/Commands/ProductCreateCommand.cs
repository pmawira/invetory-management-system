using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Products.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.Products.Commands
{
    public class ProductCreateCommand: IRequest<ProductDTO>
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public int CategoryId { get; set; }
    }
}
