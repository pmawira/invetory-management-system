using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Products.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.Categories.Database
{
    public interface ICategoryRepository: IBaseGenericRepository<Category>
    {
       public Task<bool> ExistsByID(int? id);
        Task<Product> GetById(int id);
    }
}
