using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("product_package")]
    public partial class ProductPackage : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("branch_id")]
        public int? BranchId { get; set; }
        [Column("prod_id")]
        public int? ProdId { get; set; }
        [Column("warehouse_quantity")]
        public int? WarehouseQuantity { get; set; }
        [Column("dep_quantity")]
        public int? DepQuantity { get; set; }
        [Column("expiration_date", TypeName = "date")]
        public DateTime? ExpirationDate { get; set; }
        [Column("volume")]
        public int? Volume { get; set; }

        [ForeignKey(nameof(BranchId))]
        [InverseProperty("ProductPackages")]
        public virtual Branch Branch { get; set; }
        [ForeignKey(nameof(ProdId))]
        [InverseProperty(nameof(Product.ProductPackages))]
        public virtual Product Prod { get; set; }
    }
}
