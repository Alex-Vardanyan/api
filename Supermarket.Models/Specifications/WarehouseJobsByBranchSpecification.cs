using Supermarket.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.Specifications
{
    public class WarehouseJobsByBranchSpecification : BaseSpecification<WarehouseJob>
    {
        public WarehouseJobsByBranchSpecification(int? id) : base( x => (!id.HasValue || x.BranchId == id) && x.Jobs.Status == false)
        {
            AddInclude(x => x.Jobs);
            AddInclude(x => x.Branch.Location);
        }
    }
}
