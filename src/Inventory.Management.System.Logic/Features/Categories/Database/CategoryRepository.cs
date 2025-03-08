using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Products.DTO;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> ExistsByID(int? id)
        {
            return await Uow.Context.Set<Category>()
                .AnyAsync(c => c.Id == id);
        }
        public async Task<Product?> GetById(int id)
        {
            return await Uow.Context.Set<Product>()
              .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
