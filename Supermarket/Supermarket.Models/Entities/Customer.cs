using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("customer")]
    public partial class Customer
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("first_name")]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Column("last_name")]
        [StringLength(30)]
        public string LastName { get; set; }
        [Column("birth_date", TypeName = "date")]
        public DateTime? BirthDate { get; set; }
        [Column("gender")]
        [StringLength(50)]
        public string Gender { get; set; }
        [Column("phone_number")]
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        [Column("address_id")]
        public int? AddressId { get; set; }
        [Column("created_date", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty(nameof(AddressLocation.Customers))]
        public virtual AddressLocation Address { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Customer")]
        public virtual User User { get; set; }
    }
}
