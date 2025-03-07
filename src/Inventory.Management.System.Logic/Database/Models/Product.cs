﻿using System;
using System.Collections.Generic;

namespace Inventory.Management.System.Logic.Database.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;
}
