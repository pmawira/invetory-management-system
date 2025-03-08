using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.StockAdditions.Database
{
    public interface IStockAdditionRepository: IBaseGenericRepository<StockAddition>
    {
        Task<StockAddition> GetById(int id);
    }
}
