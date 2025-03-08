using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.StockAdditions.Database
{
    public class StockAdditionRepository: BaseGenericRepository<StockAddition>, IStockAdditionRepository
    {
        public StockAdditionRepository(IUnitOfWork uow) : base(uow)
        {
        }
        public async Task<StockAddition?> GetById(int id)
        {
            return await Uow.Context.Set<StockAddition>()
              .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
