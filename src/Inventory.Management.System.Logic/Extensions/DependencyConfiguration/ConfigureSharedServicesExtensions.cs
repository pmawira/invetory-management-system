using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Extensions.DependencyConfiguration
{
    public static class ConfigureSharedServicesExtensions
    {
        public static void ConfigureSharedServices(this IServiceCollection collection, IHostEnvironment environment,
        IConfiguration config, string databaseString)
        {
            collection.ConfigureDatabaseSQLServer(environment, databaseString);
            collection.ConfigureMapper();
            collection.ConfigureMediatR();
            collection.ConfigureCoreSystemServices();
        }
    }
}
