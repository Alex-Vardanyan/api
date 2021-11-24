using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket.Api.Dtos;
using Supermarket.Api.Errors;
using Supermarket.Api.Helpers;
using Supermarket.Dal.EfStructures;
using Supermarket.Models.Entities;
using Supermarket.Models.Interfaces;
using Supermarket.Models.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Api.Controllers
{
    public class ProductsController : BaseApiController 
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<Supplier> _productSupplierRepo;
        private readonly IGenericRepository<Category> _productCategoryRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
            IGenericRepository<Supplier> productSupplierRepo, IGenericRepository<Category> productCategoryRepo,
            IMapper mapper)
        {
            _productsRepo = productsRepo;
            _productSupplierRepo = productSupplierRepo;
            _productCategoryRepo = productCategoryRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductsWithSupplierAndCategorySpecification(productParams);

            var countSpec = new ProductsWithSupplierAndCategorySpecification(productParams);
            var totalItems = await _productsRepo.CountAsync(countSpec);

            var Products = await _productsRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);

            return (new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithSupplierAndCategorySpecification(id);
            var Product = await _productsRepo.GetEntityWithSpec(spec);

            if(Product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return _mapper.Map<Product, ProductToReturnDto>(Product);
        }

        [HttpGet("Suppliers")]
        public async Task<ActionResult<Supplier>> GetSuppliers()
        {
            return Ok(await _productSupplierRepo.ListAllAsync());
        }

        [HttpGet("Categories")]
        public async Task<ActionResult<Category>> GetCategories()
        {
            return Ok(await _productCategoryRepo.ListAllAsync());
        }
    }
}
