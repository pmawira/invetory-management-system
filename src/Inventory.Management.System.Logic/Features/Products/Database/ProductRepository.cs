using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.Products.Database
{
    public class ProductRepository : BaseGenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork uow) : base(uow)
        {
        }
        public async Task<Product?> GetProductByName(string name)
        {
            return await Uow.Context.Set<Product>()
                .FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
