using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("supplier")]
    public partial class Supplier : BaseEntity
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
            Shippings = new HashSet<Shipping>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(25)]
        public string Name { get; set; }
        [Column("location_id")]
        public int? LocationId { get; set; }
        [Column("contract_exp_date", TypeName = "date")]
        public DateTime? ContractExpDate { get; set; }
        [Column("contact_num")]
        public int? ContactNum { get; set; }
        [Column("contact_email")]
        [StringLength(25)]
        public string ContactEmail { get; set; }

        [ForeignKey(nameof(LocationId))]
        [InverseProperty(nameof(AddressLocation.Suppliers))]
        public virtual AddressLocation Location { get; set; }
        [InverseProperty(nameof(Product.Supplier))]
        public virtual ICollection<Product> Products { get; set; }
        [InverseProperty(nameof(Shipping.Supplier))]
        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}
