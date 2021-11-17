using AutoMapper;
using Supermarket.Api.Dtos;
using Supermarket.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(x => x.Category, o => o.MapFrom(s => s.Category.Description))
                .ForMember(x => x.Supplier, o => o.MapFrom(s => s.Supplier.Name));
        }
    }
}
