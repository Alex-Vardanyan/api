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
        public ProductsWithSupplierAndCategorySpecification(ProductSpecParams productParams)
            : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.SupplierId == productParams.BrandId) &&
            (!productParams.DepartmentId.HasValue || x.Category.DepartmentId == productParams.DepartmentId)
            )
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Supplier);
            AddInclude(x => x.Category.Department);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "pricaAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceAsc":
                        AddOrderByDesc(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductsWithSupplierAndCategorySpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Supplier);
        }
    }
}