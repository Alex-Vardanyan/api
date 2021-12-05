using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class SuppliersController : BaseApiController
    {
        private readonly IGenericRepository<Supplier> _supplierRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepo;

        public SuppliersController(IGenericRepository<Supplier> supplierRepo, IMapper mapper, IGenericRepository<Product> productRepo)
        {
           _supplierRepo = supplierRepo;
           _mapper = mapper;
           _productRepo = productRepo;
        }

        [HttpGet]
        [Authorize(Policy = "Warehouse Admin")]
        public async Task<ActionResult<IReadOnlyList<SupplierToReturnDto>>> GetSuppliers([FromQuery]SupplierSpecParams supplierParams)
        {
            var spec = new SuppliersWithFiltersSpecification(supplierParams);
            var Suppliers =  await _supplierRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Supplier>,IReadOnlyList<SupplierToReturnDto>>(Suppliers));
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Warehouse Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SupplierProductListDto>> GetSupplierProducts(int id)
        {
            var supplierSpec = new SupplierSpecification(id);
            var Supplier =  await _supplierRepo.GetEntityWithSpec(supplierSpec);
            if (Supplier == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var ProductsToReturn = (IReadOnlyList<ProductFullInfoDto>)_mapper.Map<ICollection<Product>, ICollection<ProductFullInfoDto>>(Supplier.Products);

            return new SupplierProductListDto {
            Name = Supplier.Name,
            ContractExpDate = Supplier.ContractExpDate,
            ContactNum = Supplier.ContactNum,
            ContactEmail = Supplier.ContactEmail,
            City = Supplier.Location.City,
            District = Supplier.Location.District,
            Street = Supplier.Location.Street,
            BuildingNumber = Supplier.Location.BuildingNumber,
            Products = ProductsToReturn
            };
        }
    }
}
