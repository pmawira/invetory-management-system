using Inventory.Management.System.Logic.Database;

namespace Inventory.Management.System.Logic.DataAccess
{
    public interface IUnitOfWork
    {
        InventoryManagementSystemContext Context { get; }
        Task SaveChanges(CancellationToken cancellationToken);
    }
}
