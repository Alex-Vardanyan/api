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
        private readonly IGenericRepository<ProductPackage> _packageRepo;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductPackage> packageRepo,
            ApplicationDbContext context, IMapper mapper)
        {
            _productsRepo = productsRepo;
            _packageRepo = packageRepo;
            _context = context;
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
            //var spec = new ProductsWithSupplierAndCategorySpecification(id);
            var spec = new PackagesWithProductsSpecifictation(id);
            var Packages = await _packageRepo.ListAsync(spec);

            if(Packages.Count() == 0)
            {
                return NotFound(new ApiResponse(404));
            }
            var Product = Packages.GroupBy(x => x.Prod).Select(x => new { x.Key, Amount = x.Sum(b => b.WarehouseQuantity) }).FirstOrDefault();
            return new ProductToReturnDto
            {
                Id = Product.Key.Id,
                Name = Product.Key.Name,
                Description = Product.Key.Description,
                Price = Product.Key.Price,
                Code = Product.Key.Code,
                Department = Product.Key.Category.Department.Name,
                Supplier = Product.Key.Supplier.Name,
                ImageUrl = Product.Key.ImageUrl,
                Amount = Product.Amount,
            };

            //var Packages = await _context.ProductPackages.Where(x => x.ProdId == id).GroupBy(x => x.Prod)
                //.Select(x => new { x.Key, Amount = x.Sum(c => c.WarehouseQuantity) }).Include(x => x.Key).FirstOrDefaultAsync();
            //return _mapper.Map<Product, ProductToReturnDto>(Product);
            //return Ok(Packages);
        }

    }
}
