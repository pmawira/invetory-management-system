﻿using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Inventory.Management.System.Logic.Features.Products.Database
{
    public class ProductRepository : BaseGenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<Product?> GetById(int id)
        {
            return await Uow.Context.Set<Product>()
              .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetByName(string name)
        {
            return await Uow.Context.Set<Product>()
                .FirstOrDefaultAsync(p => p.Name == name);
        }

    }
    
}
