using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Supermarket.Models.Entities
{
    [Table("branch")]
    public partial class Branch : BaseEntity
    {
        public Branch()
        {
            Cashboxes = new HashSet<Cashbox>();
            CellProducts = new HashSet<CellProduct>();
            DisposePackages = new HashSet<DisposePackage>();
            Employees = new HashSet<Employee>();
            LogisticDestinationBranches = new HashSet<Logistic>();
            LogisticStartingBranches = new HashSet<Logistic>();
            Orders = new HashSet<Order>();
            ProductPackages = new HashSet<ProductPackage>();
            Shippings = new HashSet<Shipping>();
            WarehouseJobs = new HashSet<WarehouseJob>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("location_id")]
        public int? LocationId { get; set; }
        [Column("frezer_volume")]
        public int? FrezerVolume { get; set; }
        [Column("storage_volume")]
        public int? StorageVolume { get; set; }
        [Column("type")]
        [StringLength(25)]
        public string Type { get; set; }

        [ForeignKey(nameof(LocationId))]
        [InverseProperty(nameof(AddressLocation.Branches))]
        public virtual AddressLocation Location { get; set; }
        [InverseProperty(nameof(Cashbox.Branch))]
        public virtual ICollection<Cashbox> Cashboxes { get; set; }
        [InverseProperty(nameof(CellProduct.Branch))]
        public virtual ICollection<CellProduct> CellProducts { get; set; }
        [InverseProperty(nameof(DisposePackage.Branch))]
        public virtual ICollection<DisposePackage> DisposePackages { get; set; }
        [InverseProperty(nameof(Employee.Branch))]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty(nameof(Logistic.DestinationBranch))]
        public virtual ICollection<Logistic> LogisticDestinationBranches { get; set; }
        [InverseProperty(nameof(Logistic.StartingBranch))]
        public virtual ICollection<Logistic> LogisticStartingBranches { get; set; }
        [InverseProperty(nameof(Order.Branch))]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty(nameof(ProductPackage.Branch))]
        public virtual ICollection<ProductPackage> ProductPackages { get; set; }
        [InverseProperty(nameof(Shipping.DestinationBranch))]
        public virtual ICollection<Shipping> Shippings { get; set; }

        [InverseProperty(nameof(WarehouseJob.Branch))]
        public virtual ICollection<WarehouseJob> WarehouseJobs { get; set; }
    }
}
