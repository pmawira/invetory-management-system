using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Database.Models;
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
        public Product? GetProductByName(string name)
        {
            return Uow.Context.Set<Product>()
                .FirstOrDefault(p => p.Name == name);
        }
    }
}
