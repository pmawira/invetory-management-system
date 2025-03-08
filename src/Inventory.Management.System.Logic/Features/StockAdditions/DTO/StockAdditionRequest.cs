using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.StockAdditions.DTO
{
    public class StockAdditionRequest
    {
        public int ProductId { get; set; }
        public int QuantityAdded { get; set; }
    }
}
