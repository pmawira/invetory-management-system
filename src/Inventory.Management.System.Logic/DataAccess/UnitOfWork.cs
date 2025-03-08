using Inventory.Management.System.Logic.Database;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public InventoryManagementSystemContext Context { get; }
        private readonly ILogger<IUnitOfWork> _logger;

        public UnitOfWork(InventoryManagementSystemContext context, ILogger<UnitOfWork> logger)
        {
            Context = context;
            _logger = logger;
        }
        public async Task SaveChanges(CancellationToken cancellationToken)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
