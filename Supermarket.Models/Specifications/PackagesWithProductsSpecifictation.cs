using Supermarket.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.Specifications
{
    public class PackagesWithProductsSpecifictation : BaseSpecification<ProductPackage>
    {
        public PackagesWithProductsSpecifictation(int id) : base(x => x.ProdId == id)
        {
            AddInclude(x => x.Prod);
        }
    }
}
