using Supermarket.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Api.Dtos
{
    public class WarehousePackagesToReturnDto
    {
        public string Type { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public int? BuildingNumber { get; set; }
        public int? FrezerVolume { get; set; }
        public int? StorageVolume { get; set; }
        public IReadOnlyList<PackageToReturnDto> Packages {get; set;}
    }
}
