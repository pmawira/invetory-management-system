using AutoMapper;
using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Products.DTO;
using Inventory.Management.System.Logic.Features.StockAdditions.DTO;
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
            CreateMap<StockAdditionRequest, StockAddition>()
           .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore ID (DB-generated)
        }
    }
}
