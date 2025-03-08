using Inventory.Management.System.Logic.Extensions.DependencyConfiguration;

namespace Inventory.Management.System.API.SystemConfiguration
{
    public static class ConfigureSystemServicesExtensions
    {
        public static void ConfigureSystemServices(this IServiceCollection collection, IHostEnvironment environment,
       IConfiguration config, string databaseConnectionString)
        {
            // Register expected services for injection
           collection.ConfigureSharedServices(environment, config, databaseConnectionString);

        }
    }
}
