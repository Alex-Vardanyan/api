using Supermarket.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.Specifications
{
    public class WarehousesWithLocationSpecification : BaseSpecification<Branch>
    {
        public WarehousesWithLocationSpecification()
        {
            AddInclude(x => x.Location);
        }

        public WarehousesWithLocationSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Location);
        }
    }
}
