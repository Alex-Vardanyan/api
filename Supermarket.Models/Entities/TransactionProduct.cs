using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("transaction_product")]
    public partial class TransactionProduct : BaseEntity
    {
        [Key]
        [Column("transaction_id")]
        public int TransactionId { get; set; }
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int? Quantity { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("TransactionProducts")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(TransactionId))]
        [InverseProperty(nameof(CashboxTransaction.TransactionProducts))]
        public virtual CashboxTransaction Transaction { get; set; }
    }
}
