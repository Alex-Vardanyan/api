using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("product")]
    public partial class Product : BaseEntity
    {
        public Product()
        {
            CellProducts = new HashSet<CellProduct>();
            DisposePackages = new HashSet<DisposePackage>();
            Logistics = new HashSet<Logistic>();
            OrderProducts = new HashSet<OrderProduct>();
            ProductPackages = new HashSet<ProductPackage>();
            Shippings = new HashSet<Shipping>();
            TransactionProducts = new HashSet<TransactionProduct>();
            WarehouseToDepartmentJobs = new HashSet<WarehouseToDepartmentJob>();
            WishLists = new HashSet<WishList>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("description")]
        [StringLength(500)]
        public string Description { get; set; }
        [Column("price", TypeName = "money")]
        public decimal? Price { get; set; }
        [Column("cost", TypeName = "money")]
        public decimal? Cost { get; set; }
        [Column("code")]
        public int? Code { get; set; }
        [Column("volume")]
        public int? Volume { get; set; }
        [Column("refunded")]
        public bool? Refunded { get; set; }
        [Column("supplier_id")]
        public int? SupplierId { get; set; }
        [Column("category_id")]
        public int? CategoryId { get; set; }
        [Column("image_url")]
        [StringLength(250)]
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("Products")]
        public virtual Supplier Supplier { get; set; }
        [InverseProperty("Product")]
        public virtual SpecialCare SpecialCare { get; set; }
        [InverseProperty(nameof(CellProduct.Product))]
        public virtual ICollection<CellProduct> CellProducts { get; set; }
        [InverseProperty(nameof(DisposePackage.Product))]
        public virtual ICollection<DisposePackage> DisposePackages { get; set; }
        [InverseProperty(nameof(Logistic.Product))]
        public virtual ICollection<Logistic> Logistics { get; set; }
        [InverseProperty(nameof(OrderProduct.Product))]
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        [InverseProperty(nameof(ProductPackage.Prod))]
        public virtual ICollection<ProductPackage> ProductPackages { get; set; }
        [InverseProperty(nameof(Shipping.Product))]
        public virtual ICollection<Shipping> Shippings { get; set; }
        [InverseProperty(nameof(TransactionProduct.Product))]
        public virtual ICollection<TransactionProduct> TransactionProducts { get; set; }
        [InverseProperty(nameof(WarehouseToDepartmentJob.Product))]
        public virtual ICollection<WarehouseToDepartmentJob> WarehouseToDepartmentJobs { get; set; }
        [InverseProperty(nameof(WishList.Product))]
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
