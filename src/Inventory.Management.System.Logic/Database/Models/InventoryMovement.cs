using System;
using System.Collections.Generic;

namespace Inventory.Management.System.Logic.Database.Models;

public partial class InventoryMovement
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public string MovementType { get; set; } = null!;

    public DateTime? DateMoved { get; set; }

    public virtual Product Product { get; set; } = null!;
}
