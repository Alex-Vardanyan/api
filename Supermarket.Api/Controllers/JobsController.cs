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
    public class JobsController : BaseApiController
    {
        private readonly IGenericRepository<WarehouseJob> _jobsRepo;
        private readonly IMapper _mapper;

        public JobsController(IGenericRepository<WarehouseJob> jobsRepo, IMapper mapper)
        {
            _jobsRepo = jobsRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "Warehouse")]
        public async Task<ActionResult<IReadOnlyList<JobsToReturnDto>>> GetJobs(int? id)
        {
            var spec = new WarehouseJobsByBranchSpecification(id);
            var Jobs = await _jobsRepo.ListAsync(spec);
            if (Jobs.Count == 0)
            {
                return Ok(new {message = "No jobs here..." });
            }

            return Ok(_mapper.Map<IReadOnlyList<WarehouseJob>, IReadOnlyList<JobsToReturnDto>>(Jobs));
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Warehouse")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JobsToReturnDto>> GetJobById(int id)
        {
            var spec = new WarehouseJobsByIdSpecification(id);
            var Job = await _jobsRepo.GetEntityWithSpec(spec);
            
            if(Job == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return _mapper.Map<WarehouseJob,JobsToReturnDto>(Job);
        }
    }
}
