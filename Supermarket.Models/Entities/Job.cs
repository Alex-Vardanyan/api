using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("jobs")]
    public partial class Job : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("priority")]
        public int? Priority { get; set; }
        [Column("employee_id")]
        public int? EmployeeId { get; set; }
        [Column("status")]
        public bool? Status { get; set; }
        [Column("description")]
        [StringLength(64)]
        public string Description { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(User.Jobs))]
        public virtual User Employee { get; set; }
        [InverseProperty("Jobs")]
        public virtual CheckAndDisposeJob CheckAndDisposeJob { get; set; }
        [InverseProperty("Jobs")]
        public virtual WarehouseJob WarehouseJob { get; set; }
        [InverseProperty("Jobs")]
        public virtual WarehouseToDepartmentJob WarehouseToDepartmentJob { get; set; }
    }
}
