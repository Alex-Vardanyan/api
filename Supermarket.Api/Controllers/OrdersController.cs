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
using System.Security.Claims;
using System.Threading.Tasks;

namespace Supermarket.Api.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Order>> GetBasket()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var userSpec = new UserOverviewSpecification(email);
            User user = await _unitOfWork.Repository<User>().GetEntityWithSpec(userSpec);
            if (user == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            var spec = new OrderWithUserSpecification(user);
            var Products = await _unitOfWork.Repository<OrderProduct>().ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<OrderProduct>, IReadOnlyList<OrderDto>>(Products));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Order()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var userSpec = new UserOverviewSpecification(email);
            User user = await _unitOfWork.Repository<User>().GetEntityWithSpec(userSpec);
            if (user == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            var orderStatus = new OrderStatus
            {
                Name = "Ordered"
            };
            if (_unitOfWork.Repository<OrderStatus>().GetByIdAsync(2)== null)
            {
                _unitOfWork.Repository<OrderStatus>().Add(orderStatus);
            }
            var spec = new OrderWithUserSpecification(user);
            var Products = await _unitOfWork.Repository<OrderProduct>().ListAsync(spec);
            foreach (var product in Products)
            {
                product.Order.OrderStatus = orderStatus;
                _unitOfWork.Repository<Order>().Update(product.Order);
            }
            var result = await _unitOfWork.Complete();
            if (result == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok();
        }
    }
}
