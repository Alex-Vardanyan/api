using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("proffesion")]
    public partial class Proffesion
    {
        public Proffesion()
        {
            Employees = new HashSet<Employee>();
            OpenPositions = new HashSet<OpenPosition>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("prof_name")]
        [StringLength(50)]
        public string ProfName { get; set; }

        [InverseProperty(nameof(Employee.Profession))]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty(nameof(OpenPosition.Position))]
        public virtual ICollection<OpenPosition> OpenPositions { get; set; }
    }
}
