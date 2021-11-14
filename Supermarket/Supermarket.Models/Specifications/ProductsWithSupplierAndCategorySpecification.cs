using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Supermarket.Models.Entities;

namespace Supermarket.Models.Specifications
{
    public class ProductsWithSupplierAndCategorySpecification : BaseSpecification<Product>
    {
        public ProductsWithSupplierAndCategorySpecification()
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Supplier);
        }

        public ProductsWithSupplierAndCategorySpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Supplier);
        }
    }
}