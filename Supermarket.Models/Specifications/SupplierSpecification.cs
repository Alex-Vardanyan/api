using Supermarket.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.Specifications
{
    public class SupplierSpecification : BaseSpecification<Supplier>
    {
        public SupplierSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Location);
            AddInclude(x => x.Products);
        }
    }
}
