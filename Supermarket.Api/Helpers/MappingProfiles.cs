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
                .ForMember(x => x.Department, o => o.MapFrom(s => s.Category.Department.Name))
                .ForMember(x => x.Supplier, o => o.MapFrom(s => s.Supplier.Name))
                .ForMember(x => x.ImageUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Branch, WarehouseToReturnDto>()
                .ForMember(x => x.City, o => o.MapFrom(s => s.Location.City))
                .ForMember(x => x.District, o => o.MapFrom(s => s.Location.District))
                .ForMember(x => x.Street, o => o.MapFrom(s => s.Location.Street))
                .ForMember(x => x.BuildingNumber, o => o.MapFrom(s => s.Location.BuildingNumber));

            CreateMap<ProductPackage, PackageToReturnDto>()
                .ForMember(x => x.Prod, o => o.MapFrom(s => s.Prod.Name))
                .ForMember(x => x.FrVolume, o => o.MapFrom(s => s.Prod.SpecialCare == null ? null : s.Prod.Volume))
                .ForMember(x => x.Volume, o => o.MapFrom(s => s.Prod.SpecialCare == null ? s.Prod.Volume : null));

            CreateMap<WarehouseJob, JobsToReturnDto>()
                .ForMember(x => x.Priority, o => o.MapFrom(s => s.Jobs.Priority))
                .ForMember(x => x.Description, o => o.MapFrom(s => s.Jobs.Description))
                .ForMember(x => x.City, o => o.MapFrom(s => s.Branch.Location.City))
                .ForMember(x => x.District, o => o.MapFrom(s => s.Branch.Location.District))
                .ForMember(x => x.Street, o => o.MapFrom(s => s.Branch.Location.Street))
                .ForMember(x => x.BuildingNumber, o => o.MapFrom(s => s.Branch.Location.BuildingNumber));

            CreateMap<Supplier, SupplierToReturnDto>()
                .ForMember(x => x.City, o => o.MapFrom(s => s.Location.City))
                .ForMember(x => x.District, o => o.MapFrom(s => s.Location.District))
                .ForMember(x => x.Street, o => o.MapFrom(s => s.Location.Street))
                .ForMember(x => x.BuildingNumber, o => o.MapFrom(s => s.Location.BuildingNumber));

            CreateMap<Product, ProductFullInfoDto>()
                .ForMember(x => x.Category, o => o.MapFrom(s => s.Category.Description))
                .ForMember(x => x.Department, o => o.MapFrom(s => s.Category.Department.Name));
        }
    }
}
