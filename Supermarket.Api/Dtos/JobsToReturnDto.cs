using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Api.Dtos
{
    public class JobsToReturnDto
    {
        public int JobsId { get; set; }
        public int? Priority { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public int? BuildingNumber { get; set; }
    }
}
