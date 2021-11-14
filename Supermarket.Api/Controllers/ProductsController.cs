using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    [Controller]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<Supplier> _productSupplierRepo;
        private readonly IGenericRepository<Category> _productCategoryRepo;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<Supplier> productSupplierRepo, IGenericRepository<Category> productCategoryRepo)
        {
            _productsRepo = productsRepo;
            _productSupplierRepo = productSupplierRepo;
            _productCategoryRepo = productCategoryRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var spec = new ProductsWithSupplierAndCategorySpecification();
            var Products = await _productsRepo.ListAsync(spec);

            return Ok(Products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduc(int id)
        {
            var spec = new ProductsWithSupplierAndCategorySpecification(id);
            return await _productsRepo.GetEntityWithSpec(spec);
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
