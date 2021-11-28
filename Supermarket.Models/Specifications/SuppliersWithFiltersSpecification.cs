using Supermarket.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.Specifications
{
    public class SuppliersWithFiltersSpecification : BaseSpecification<Supplier>
    {
        public SuppliersWithFiltersSpecification(SupplierSpecParams supplierParams) : 
            base(x =>
            (string.IsNullOrEmpty(supplierParams.Search) || x.Name.ToLower().Contains(supplierParams.Search)))
        {
            AddOrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(supplierParams.Sort))
            {
                switch (supplierParams.Sort)
                {
                    case "ExpDateAsc":
                        AddOrderBy(x => x.ContractExpDate);
                        break;
                    case "ExpDateDesc":
                        AddOrderByDesc(x => x.ContractExpDate);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }
    }
}
