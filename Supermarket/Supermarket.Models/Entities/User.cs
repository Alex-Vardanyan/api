using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("users")]
    public partial class User : BaseEntity
    {
        public User()
        {
            Employees = new HashSet<Employee>();
            Jobs = new HashSet<Job>();
            LogSessions = new HashSet<LogSession>();
            OrderCustomers = new HashSet<Order>();
            OrderDeliveryMen = new HashSet<Order>();
            WishLists = new HashSet<WishList>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("email")]
        [StringLength(64)]
        public string Email { get; set; }
        [Column("username")]
        [StringLength(20)]
        public string Username { get; set; }
        [Column("passwd")]
        [StringLength(64)]
        public string Passwd { get; set; }

        [InverseProperty("User")]
        public virtual Customer Customer { get; set; }
        [InverseProperty("Employee")]
        public virtual Deliveryman Deliveryman { get; set; }
        [InverseProperty(nameof(Employee.Users))]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty(nameof(Job.Employee))]
        public virtual ICollection<Job> Jobs { get; set; }
        [InverseProperty(nameof(LogSession.User))]
        public virtual ICollection<LogSession> LogSessions { get; set; }
        [InverseProperty(nameof(Order.Customer))]
        public virtual ICollection<Order> OrderCustomers { get; set; }
        [InverseProperty(nameof(Order.DeliveryMan))]
        public virtual ICollection<Order> OrderDeliveryMen { get; set; }
        [InverseProperty(nameof(WishList.Customer))]
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
