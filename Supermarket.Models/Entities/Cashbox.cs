using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("cashbox")]
    public partial class Cashbox : BaseEntity
    {
        public Cashbox()
        {
            CashboxTransactions = new HashSet<CashboxTransaction>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("branch_id")]
        public int? BranchId { get; set; }
        [Column("money", TypeName = "money")]
        public decimal? Money { get; set; }

        [ForeignKey(nameof(BranchId))]
        [InverseProperty("Cashboxes")]
        public virtual Branch Branch { get; set; }
        [InverseProperty(nameof(CashboxTransaction.Cashbox))]
        public virtual ICollection<CashboxTransaction> CashboxTransactions { get; set; }
    }
}
