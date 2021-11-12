using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("delivery_status")]
    public partial class DeliveryStatus
    {
        public DeliveryStatus()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(11)]
        public string Name { get; set; }

        [InverseProperty(nameof(Order.DeliveryStatus))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
