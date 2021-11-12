using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("warehouse_jobs")]
    public partial class WarehouseJob
    {
        [Key]
        [Column("jobs_id")]
        public int JobsId { get; set; }
        [Column("shipping_id")]
        public int? ShippingId { get; set; }
        [Column("logistics_id")]
        public int? LogisticsId { get; set; }

        [ForeignKey(nameof(JobsId))]
        [InverseProperty(nameof(Job.WarehouseJob))]
        public virtual Job Jobs { get; set; }
        [ForeignKey(nameof(LogisticsId))]
        [InverseProperty(nameof(Logistic.WarehouseJobs))]
        public virtual Logistic Logistics { get; set; }
        [ForeignKey(nameof(ShippingId))]
        [InverseProperty("WarehouseJobs")]
        public virtual Shipping Shipping { get; set; }
    }
}
