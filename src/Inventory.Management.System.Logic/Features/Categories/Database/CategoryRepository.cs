using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.Categories.Database
{
    public class CategoryRepository: BaseGenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
