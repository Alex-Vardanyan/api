using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("order")]
    public partial class Order : BaseEntity
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("customer_id")]
        public int? CustomerId { get; set; }
        [Column("delivery_status_id")]
        public int? DeliveryStatusId { get; set; }
        [Column("order_status_id")]
        public int? OrderStatusId { get; set; }
        [Column("delivery_man_id")]
        public int? DeliveryManId { get; set; }
        [Column("order_description")]
        [StringLength(255)]
        public string OrderDescription { get; set; }
        [Column("branch_id")]
        public int? BranchId { get; set; }
        [Column("peyment_status")]
        public bool? PeymentStatus { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("delivered", TypeName = "datetime")]
        public DateTime? Delivered { get; set; }

        [ForeignKey(nameof(BranchId))]
        [InverseProperty("Orders")]
        public virtual Branch Branch { get; set; }
        [ForeignKey(nameof(CustomerId))]
        [InverseProperty(nameof(User.OrderCustomers))]
        public virtual User Customer { get; set; }
        [ForeignKey(nameof(DeliveryManId))]
        [InverseProperty(nameof(User.OrderDeliveryMen))]
        public virtual User DeliveryMan { get; set; }
        [ForeignKey(nameof(DeliveryStatusId))]
        [InverseProperty("Orders")]
        public virtual DeliveryStatus DeliveryStatus { get; set; }
        [ForeignKey(nameof(OrderStatusId))]
        [InverseProperty("Orders")]
        public virtual OrderStatus OrderStatus { get; set; }
        [InverseProperty(nameof(OrderProduct.Order))]
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
