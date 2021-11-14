using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("order_status")]
    public partial class OrderStatus : BaseEntity
    {
        public OrderStatus()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(8)]
        public string Name { get; set; }

        [InverseProperty(nameof(Order.OrderStatus))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
