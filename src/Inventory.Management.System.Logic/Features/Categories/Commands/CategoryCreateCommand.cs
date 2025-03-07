using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.Categories.Commands
{
    public class CategoryCreateCommand: IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
