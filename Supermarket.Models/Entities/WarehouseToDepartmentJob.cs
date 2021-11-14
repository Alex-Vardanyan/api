using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("warehouseToDepartment_jobs")]
    public partial class WarehouseToDepartmentJob : BaseEntity
    {
        [Key]
        [Column("jobs_id")]
        public int JobsId { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int? Quantity { get; set; }

        [ForeignKey(nameof(JobsId))]
        [InverseProperty(nameof(Job.WarehouseToDepartmentJob))]
        public virtual Job Jobs { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("WarehouseToDepartmentJobs")]
        public virtual Product Product { get; set; }
    }
}
