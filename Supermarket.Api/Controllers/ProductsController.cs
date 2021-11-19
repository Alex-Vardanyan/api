using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supermarket.Api.Dtos;
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
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithSupplierAndCategorySpecification();
            var Products = await _productsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithSupplierAndCategorySpecification(id);
            var Product = await _productsRepo.GetEntityWithSpec(spec);
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
