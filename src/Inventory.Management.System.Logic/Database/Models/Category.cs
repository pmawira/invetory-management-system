using System;
using System.Collections.Generic;

namespace Inventory.Management.System.Logic.Database.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
