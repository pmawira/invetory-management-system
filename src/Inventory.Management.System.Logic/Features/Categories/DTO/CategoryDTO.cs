using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.Categories.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string? Description { get; set; }
    }
}
