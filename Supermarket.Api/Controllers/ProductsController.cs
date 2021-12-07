using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace Supermarket.Api.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductPackage> _packageRepo;
        private readonly IGenericRepository<User> _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductPackage> packageRepo,
            IGenericRepository<User> userRepo, IUnitOfWork unitOfWork,IMapper mapper)
        {
            _productsRepo = productsRepo;
            _packageRepo = packageRepo;
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
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

            if (Packages.Count() == 0)
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
        [Authorize]
        [HttpPost("{id}")]
        public async Task<ActionResult<Product>> AddToOrderList(int id, int quantity = 1)
        {
            var product = await _productsRepo.GetByIdAsync(id);
            if (product == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var spec = new UserOverviewSpecification(email);
            User user = await _userRepo.GetEntityWithSpec(spec);
            if (user == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            var orderStatus = new OrderStatus
            {
                Name = "In Basket"
            };
            if (await _unitOfWork.Repository<OrderStatus>().GetByIdAsync(1) == null)
            {
                _unitOfWork.Repository<OrderStatus>().Add(orderStatus);
            }


            var order = new Order
            {
                Customer = user,
                CreatedAt = DateTime.Now,
                OrderStatus = orderStatus
            };
            var orderProduct = new OrderProduct
            {
                Order = order,
                Product = product,
                Quantity = quantity,
                Total = (int?)(product.Price.Value * quantity)
            };

            _unitOfWork.Repository<Order>().Add(order);
            _unitOfWork.Repository<OrderProduct>().Add(orderProduct);
            var result = await _unitOfWork.Complete();
            if (result == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return product;
        }
    }
}
