using AutoMapper;
using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Products.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.StockAdditions
{
    public class StockAdditionMappingProfile : Profile
    {
        public StockAdditionMappingProfile()
        {
            CreateMap<ProductCreateRequest, Product>()
           .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore ID (DB-generated)
           .ForMember(dest => dest.DateCreated, opt => opt.Ignore()) // System-managed
           .ForMember(dest => dest.DateModified, opt => opt.Ignore()) // System-managed
           .ForMember(dest => dest.Category, opt => opt.Ignore()); // Navigation property
        }
    }
}
