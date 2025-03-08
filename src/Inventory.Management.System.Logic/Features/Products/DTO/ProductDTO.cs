using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.Products.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        public int ReorderLevel { get; set; }
        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
