using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Api.Dtos
{
    public class ProductFullInfoDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? Cost { get; set; }
        public int? Code { get; set; }
        public int? Volume { get; set; }
        public bool? Refunded { get; set; }
        public string Category { get; set; }
        public string Department { get; set; }
    }
}
