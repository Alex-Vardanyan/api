using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Supermarket.Api.Dtos;
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
    public class WarehousesController : Controller
    {
        private readonly IGenericRepository<Branch> _brachesRepo;
        private readonly IMapper _mapper;

        public WarehousesController(IGenericRepository<Branch> brachesRepo,IMapper mapper)
        {
            _brachesRepo = brachesRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<WarehouseToReturnDto>>> GetBranches()
        {
            var spec = new WarehousesWithLocationSpecification();
            var Warehouses = await _brachesRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Branch>, IReadOnlyList<WarehouseToReturnDto>>(Warehouses));
        }

    }
}
