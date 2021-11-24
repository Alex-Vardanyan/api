using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supermarket.Api.Dtos;
using Supermarket.Api.Errors;
using Supermarket.Models.Entities;
using Supermarket.Models.Interfaces;
using Supermarket.Models.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Api.Controllers
{
    public class WarehousesController : BaseApiController
    {
        private readonly IGenericRepository<Branch> _brachesRepo;
        private readonly IGenericRepository<ProductPackage> _productPackagesRepo;
        private readonly IMapper _mapper;

        public WarehousesController(IGenericRepository<Branch> brachesRepo, IGenericRepository<ProductPackage> productPackagesRepo,IMapper mapper)
        {
            _brachesRepo = brachesRepo;
            _productPackagesRepo = productPackagesRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<WarehouseToReturnDto>>> GetBranches()
        {
            var spec = new WarehousesWithLocationSpecification();
            var Warehouses = await _brachesRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Branch>, IReadOnlyList<WarehouseToReturnDto>>(Warehouses));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WarehousePackagesToReturnDto>> GetBranchBy(int id)
        {
            var spec = new WarehousesWithLocationSpecification(id);
            var Warehouse = await _brachesRepo.GetEntityWithSpec(spec);
            if(Warehouse == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var productSpec = new ProductPackagesFromBranchSpecification(id);
            var ProductPackages = await _productPackagesRepo.ListAsync(productSpec);
            var PackagesToReturn = _mapper.Map<IReadOnlyList<ProductPackage>, IReadOnlyList<PackageToReturnDto>>(ProductPackages);
            return (new WarehousePackagesToReturnDto
            {
                Type = Warehouse.Type,
                StorageVolume = Warehouse.StorageVolume,
                FrezerVolume = Warehouse.FrezerVolume,
                City = Warehouse.Location.City,
                District = Warehouse.Location.District,
                Street = Warehouse.Location.Street,
                BuildingNumber = Warehouse.Location.BuildingNumber,
                Packages = PackagesToReturn
            });
        }
    }
}
