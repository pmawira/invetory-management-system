using Inventory.Management.System.Logic.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.DataAccess
{
    public interface IInventoryMovementRepository: IBaseGenericRepository<InventoryMovement>
    {
    }
}
