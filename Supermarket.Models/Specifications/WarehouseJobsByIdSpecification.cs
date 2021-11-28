using Supermarket.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.Specifications
{
    public class WarehouseJobsByIdSpecification : BaseSpecification<WarehouseJob>
    {
        public WarehouseJobsByIdSpecification(int id) : base(x => x.JobsId == id)
        {
            AddInclude(x => x.Jobs);
            AddInclude(x => x.Logistics);
            AddInclude(x => x.Shipping);
        }
    }
}
