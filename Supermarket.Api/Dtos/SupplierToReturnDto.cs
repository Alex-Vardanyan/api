using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Api.Dtos
{
    public class SupplierToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ContractExpDate { get; set; }
        public int? ContactNum { get; set; }
        public string ContactEmail { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public int? BuildingNumber { get; set; }
    }
}
