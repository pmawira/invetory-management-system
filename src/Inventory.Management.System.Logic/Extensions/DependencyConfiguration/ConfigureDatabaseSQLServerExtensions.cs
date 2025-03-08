using EntityFramework.Exceptions.SqlServer;
using Inventory.Management.System.Logic.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Extensions.DependencyConfiguration
{
    public static class ConfigureDatabaseSQLServerExtensions
    {
        public static void ConfigureDatabaseSQLServer(this IServiceCollection collection,
         IHostEnvironment environment, string databaseConnectionString)
        {
            // These gymnastics are because pooling fails for the integration tests
            // In production, use connection pooling
            if (environment.IsProduction() || environment.IsStaging())
            {
                // This will break for multi-tenancy
                collection.AddDbContextPool<InventoryManagementSystemContext>(options =>
                {
                    options.UseSqlServer(databaseConnectionString,
                        x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                    options.UseExceptionProcessor();
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

            }
            else // Use normal db context.
            {
                collection.AddDbContext<InventoryManagementSystemContext>(options =>
                {
                    options.UseSqlServer(databaseConnectionString,
                        x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                    options.UseExceptionProcessor();
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });
            }
        }
    }
}
