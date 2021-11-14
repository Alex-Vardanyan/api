using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("logistics")]
    public partial class Logistic : BaseEntity
    {
        public Logistic()
        {
            WarehouseJobs = new HashSet<WarehouseJob>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("destination_branch_id")]
        public int? DestinationBranchId { get; set; }
        [Column("starting_branch_id")]
        public int? StartingBranchId { get; set; }
        [Column("product_id")]
        public int? ProductId { get; set; }
        [Column("quantity")]
        public int? Quantity { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("sent_at", TypeName = "datetime")]
        public DateTime? SentAt { get; set; }
        [Column("arrived_at", TypeName = "datetime")]
        public DateTime? ArrivedAt { get; set; }

        [ForeignKey(nameof(DestinationBranchId))]
        [InverseProperty(nameof(Branch.LogisticDestinationBranches))]
        public virtual Branch DestinationBranch { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Logistics")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(StartingBranchId))]
        [InverseProperty(nameof(Branch.LogisticStartingBranches))]
        public virtual Branch StartingBranch { get; set; }
        [InverseProperty(nameof(WarehouseJob.Logistics))]
        public virtual ICollection<WarehouseJob> WarehouseJobs { get; set; }
    }
}
