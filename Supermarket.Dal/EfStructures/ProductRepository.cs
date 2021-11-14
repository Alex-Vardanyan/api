using Microsoft.EntityFrameworkCore;
using Supermarket.Models.Entities;
using Supermarket.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket.Dal.EfStructures
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Category>> GetProductCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            //var typeId = 1;
            //var Products = _context.Products.Where(x => x.CategoryId == typeId).Include(x => x.Category).ToListAsync();

            return await _context.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductPackage>> GetProductsByIdAsync(int id)
        {
            return await _context.ProductPackages.Include(x => x.Prod).Where(x => x.ProdId == id && x.WarehouseQuantity != 0).ToListAsync();
        }

        public async Task<IReadOnlyList<Supplier>> GetProductsSupplierAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }
    }
}
