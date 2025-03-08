using Inventory.Management.System.Logic.Features.Categories.Commands;
using Inventory.Management.System.Logic.Features.Products.Commands;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inventory.Management.System.Logic.Extensions.DependencyConfiguration
{
    public static class ConfigureMediatoRExtensions
    {
        public static void ConfigureMediatR(this IServiceCollection collection)
        {
          
            collection.AddMediatR(mediatrConfiguration =>
            {
                mediatrConfiguration.RegisterServicesFromAssemblyContaining(typeof(ProductCreateCommand));
                mediatrConfiguration.RegisterServicesFromAssemblyContaining(typeof(CategoryCreateCommand));
                var entryAssembly = Assembly.GetEntryAssembly();
                if (entryAssembly is not null)
                    mediatrConfiguration.RegisterServicesFromAssembly(entryAssembly);

                mediatrConfiguration.NotificationPublisher = new TaskWhenAllPublisher();
              
            });
        }
    }
}
