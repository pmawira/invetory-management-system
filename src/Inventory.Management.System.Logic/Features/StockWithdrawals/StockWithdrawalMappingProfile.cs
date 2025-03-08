using AutoMapper;
using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Products.DTO;
using Inventory.Management.System.Logic.Features.StockWithdrawals.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.StockWithdrawals
{
    public class StockWithdrawalMappingProfile: Profile
    {
        public StockWithdrawalMappingProfile()
        {
            CreateMap<StockWithdrawalRequest, StockWithdrawal>()
      .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore ID (DB-generated)
        }
    }
}
