using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("cashbox_transaction")]
    public partial class CashboxTransaction : BaseEntity
    {
        public CashboxTransaction()
        {
            TransactionProducts = new HashSet<TransactionProduct>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("cashier")]
        public int? Cashier { get; set; }
        [Column("cashbox_id")]
        public int? CashboxId { get; set; }
        [Required]
        [Column("date")]
        public byte[] Date { get; set; }

        [ForeignKey(nameof(CashboxId))]
        [InverseProperty("CashboxTransactions")]
        public virtual Cashbox Cashbox { get; set; }
        [ForeignKey(nameof(Cashier))]
        [InverseProperty(nameof(Employee.CashboxTransactions))]
        public virtual Employee CashierNavigation { get; set; }
        [InverseProperty(nameof(TransactionProduct.Transaction))]
        public virtual ICollection<TransactionProduct> TransactionProducts { get; set; }
    }
}
