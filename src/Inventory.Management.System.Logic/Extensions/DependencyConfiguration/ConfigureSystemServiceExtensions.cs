using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Features.Categories.Database;
using Inventory.Management.System.Logic.Features.Products.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Management.System.Logic.Extensions.DependencyConfiguration
{
    public static class ConfigureSystemServiceExtensions
    {
        public static void ConfigureCoreSystemServices(this IServiceCollection collection)
        {
            collection.AddScoped(typeof(IBaseGenericRepository<>), typeof(BaseGenericRepository<>));
            collection.AddScoped<IProductRepository, ProductRepository>();
            collection.AddScoped<ICategoryRepository, CategoryRepository>();
            collection.AddScoped<IUnitOfWork, UnitOfWork>();
        }

    }
}
