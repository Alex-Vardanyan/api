using Supermarket.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Models.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<ProductPackage>> GetProductsByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<Supplier>> GetProductsSupplierAsync();
        Task<IReadOnlyList<Category>> GetProductCategoryAsync();
    }
}
