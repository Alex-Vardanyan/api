using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("dispose_package")]
    public partial class DisposePackage : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("product_id")]
        public int? ProductId { get; set; }
        [Column("branch_id")]
        public int? BranchId { get; set; }
        [Column("quantity")]
        public int? Quantity { get; set; }
        [Column("volume")]
        public int? Volume { get; set; }

        [ForeignKey(nameof(BranchId))]
        [InverseProperty("DisposePackages")]
        public virtual Branch Branch { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("DisposePackages")]
        public virtual Product Product { get; set; }
    }
}
