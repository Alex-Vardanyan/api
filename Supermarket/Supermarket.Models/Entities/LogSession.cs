using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("log_sessions")]
    public partial class LogSession : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int? UserId { get; set; }
        [Column("log_in", TypeName = "datetime")]
        public DateTime? LogIn { get; set; }
        [Column("log_out", TypeName = "datetime")]
        public DateTime? LogOut { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("LogSessions")]
        public virtual User User { get; set; }
    }
}
