using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("open_positions")]
    public partial class OpenPosition : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("position_id")]
        public int? PositionId { get; set; }
        [Column("descrription")]
        [StringLength(250)]
        public string Descrription { get; set; }

        [ForeignKey(nameof(PositionId))]
        [InverseProperty(nameof(Proffesion.OpenPositions))]
        public virtual Proffesion Position { get; set; }
    }
}
