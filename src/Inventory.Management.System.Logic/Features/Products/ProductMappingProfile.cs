using AutoMapper;
using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Products.Commands;
using Inventory.Management.System.Logic.Features.Products.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Inventory.Management.System.Logic.Features.Products
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductCreateRequest, Product>()
             .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore ID (DB-generated)
             .ForMember(dest => dest.DateCreated, opt => opt.Ignore()) // System-managed
             .ForMember(dest => dest.DateModified, opt => opt.Ignore()) // System-managed
             .ForMember(dest => dest.Category, opt => opt.Ignore()); // Navigation property

            CreateMap<ProductCreateCommand, Product>()
           .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
           .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(_ => DateTime.UtcNow)) // Set default value
           .ForMember(dest => dest.DateModified, opt => opt.MapFrom(_ => DateTime.UtcNow)) // Set default value
           .ForMember(dest => dest.Category, opt => opt.Ignore()); // Ignore navigation property

            CreateMap<ProductCreateRequest, ProductCreateCommand>();
            CreateMap<ProductDTO, Product>();
            CreateMap<Product,ProductDTO>();
            CreateMap<ProductUpdateCommand, Product>();
        }
    }
}
