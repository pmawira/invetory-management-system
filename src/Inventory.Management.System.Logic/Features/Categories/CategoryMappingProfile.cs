using AutoMapper;
using Inventory.Management.System.Logic.Database.Models;
using Inventory.Management.System.Logic.Features.Categories.Commands;
using Inventory.Management.System.Logic.Features.Categories.DTO;
using Inventory.Management.System.Logic.Features.Products.Commands;
using Inventory.Management.System.Logic.Features.Products.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Features.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryCreateRequest, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore ID (DB-generated)
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore()) // System-managed
            .ForMember(dest => dest.DateModified, opt => opt.Ignore()); // System-managed

            CreateMap<CategoryCreateCommand, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
           .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(_ => DateTime.UtcNow)) // Set default value
           .ForMember(dest => dest.DateModified, opt => opt.MapFrom(_ => DateTime.UtcNow)); // Set default value


            CreateMap<CategoryCreateRequest, CategoryCreateCommand>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryCreateCommand, Category>();
        }
    }
}
