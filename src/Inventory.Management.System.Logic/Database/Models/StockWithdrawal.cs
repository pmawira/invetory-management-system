using System;
using System.Collections.Generic;

namespace Inventory.Management.System.Logic.Database.Models;

public partial class StockWithdrawal
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int QuantityWithdrawn { get; set; }

    public DateTime? DateWithdrawn { get; set; }

    public virtual Product Product { get; set; } = null!;
}
