using System;
using System.Collections.Generic;

namespace Inventory.Management.System.Logic.Database.Models;

public partial class StockAddition
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int QuantityAdded { get; set; }

    public DateTime? DateAdded { get; set; }

    public virtual Product Product { get; set; } = null!;
}
