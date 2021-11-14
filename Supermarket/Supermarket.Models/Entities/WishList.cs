using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("wish_list")]
    public partial class WishList : BaseEntity
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Key]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty(nameof(User.WishLists))]
        public virtual User Customer { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("WishLists")]
        public virtual Product Product { get; set; }
    }
}
