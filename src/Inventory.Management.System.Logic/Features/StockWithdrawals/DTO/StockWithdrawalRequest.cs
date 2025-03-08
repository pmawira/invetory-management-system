using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.StockWithdrawals.DTO
{
    public class StockWithdrawalRequest
    {
        public int ProductId { get; set; }
        public int QuantityWithdrawn { get; set; }
    }
}
