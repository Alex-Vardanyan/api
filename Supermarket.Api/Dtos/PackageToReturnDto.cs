using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Api.Dtos
{
    public class PackageToReturnDto
    {
        public int Id { get; set; }
        public int? WarehouseQuantity { get; set; }
        public int? DepQuantity { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? Volume { get; set; }

        public int? FrVolume { get; set; }
        public string Prod { get; set; }
    }
}
