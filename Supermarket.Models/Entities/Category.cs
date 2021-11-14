using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("category")]
    public partial class Category : BaseEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("department_id")]
        public int? DepartmentId { get; set; }
        [Column("description")]
        [StringLength(250)]
        public string Description { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("Categories")]
        public virtual Department Department { get; set; }
        [InverseProperty(nameof(Product.Category))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
