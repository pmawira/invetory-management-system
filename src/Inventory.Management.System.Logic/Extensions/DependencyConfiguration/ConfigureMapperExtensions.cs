using Inventory.Management.System.Logic.Features.Categories;
using Inventory.Management.System.Logic.Features.Products;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Extensions.DependencyConfiguration
{
    public static class ConfigureMapperExtensions
    {
        public static void ConfigureMapper(this IServiceCollection collection)
        {
            var assemblies = new[]
       {
            typeof(ProductMappingProfile).Assembly,
            typeof(CategoryMappingProfile).Assembly,
        };
            collection.AddAutoMapper(assemblies);
        }
    }
}
