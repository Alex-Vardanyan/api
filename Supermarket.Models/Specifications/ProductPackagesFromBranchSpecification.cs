using Supermarket.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.Specifications
{
    public class ProductPackagesFromBranchSpecification : BaseSpecification<ProductPackage>
    {
        public ProductPackagesFromBranchSpecification(int id) : base(x => x.BranchId == id)
        {
            AddInclude(x => x.Prod);
            AddInclude(x => x.Prod.SpecialCare);
        }
    }
}
