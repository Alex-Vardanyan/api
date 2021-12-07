using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.Api.Dtos
{
    public class OrderDto
    {
        public ProductToReturnDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
