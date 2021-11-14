using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("checkAndDispose_job")]
    public partial class CheckAndDisposeJob : BaseEntity
    {
        [Key]
        [Column("jobs_id")]
        public int JobsId { get; set; }

        [ForeignKey(nameof(JobsId))]
        [InverseProperty(nameof(Job.CheckAndDisposeJob))]
        public virtual Job Jobs { get; set; }
    }
}
