using System;
using System.Collections.Generic;

namespace Inventory.Management.System.Logic.Database.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public int CategoryId { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public int ReorderLevel { get; set; }

    public int StockQuantity { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<InventoryMovement> InventoryMovements { get; set; } = new List<InventoryMovement>();

    public virtual ICollection<StockAddition> StockAdditions { get; set; } = new List<StockAddition>();

    public virtual ICollection<StockWithdrawal> StockWithdrawals { get; set; } = new List<StockWithdrawal>();
}
