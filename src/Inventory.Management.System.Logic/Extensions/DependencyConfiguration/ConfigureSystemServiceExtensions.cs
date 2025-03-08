using Inventory.Management.System.Logic.DataAccess;
using Inventory.Management.System.Logic.Features.Categories.Database;
using Inventory.Management.System.Logic.Features.Products.Database;
using Inventory.Management.System.Logic.Features.StockAdditions.Database;
using Inventory.Management.System.Logic.Features.StockWithdrawals.Database;
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
            collection.AddScoped<IInventoryMovementRepository, InventoryMovementRepository>();
            collection.AddScoped<IStockAdditionRepository, StockAdditionRepository>();
            collection.AddScoped<IStockWithdrawalRepository, StockWithdrawRepository>();

            collection.AddScoped<IUnitOfWork, UnitOfWork>();
        }

    }
}
