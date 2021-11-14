using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("special_care")]
    public partial class SpecialCare : BaseEntity
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("max_temp")]
        public int? MaxTemp { get; set; }
        [Column("min_temp")]
        public int? MinTemp { get; set; }
        [Column("expiration_date", TypeName = "datetime")]
        public DateTime? ExpirationDate { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("SpecialCare")]
        public virtual Product Product { get; set; }
    }
}
