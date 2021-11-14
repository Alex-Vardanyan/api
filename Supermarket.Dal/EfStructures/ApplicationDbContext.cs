using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Supermarket.Models.Entities;
using Supermarket.Dal.Exceptions;

#nullable disable

namespace Supermarket.Dal.EfStructures
{
    public partial class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            base.SavingChanges += (sender, args) =>
            {
                Console.WriteLine($"Saving changes for {((ApplicationDbContext)sender)!.Database!.GetConnectionString()}");
            };
            base.SavedChanges += (sender, args) =>
            {
                Console.WriteLine($"Saved {args!.EntitiesSavedCount} changes for {((ApplicationDbContext)sender)!.Database!.GetConnectionString()}");
            };
            base.SaveChangesFailed += (sender, args) =>
            {
                Console.WriteLine($"An exception occured! {args.Exception.Message} entities");
            };
            ChangeTracker.Tracked += ChangeTracker_Tracked;
            ChangeTracker.StateChanged += ChangeTracker_StateChanged;
        }

        private void ChangeTracker_Tracked(object? sender, EntityTrackedEventArgs e)
        {
            var source = (e.FromQuery ? "Database" : "Code");
        }

        private void ChangeTracker_StateChanged(object? sender, EntityStateChangedEventArgs e)
        {
            var action = string.Empty;
            switch (e.NewState)
            {
                case EntityState.Unchanged:
                    action = e.OldState switch
                    {
                        EntityState.Added => "Added",
                        EntityState.Modified => "Edited",
                        _ => action
                    };
                    Console.WriteLine($"The object was {action}");
                    break;
            }
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                throw new CustomConcurrencyException("A concurrency error happened", ex);
            }
            catch(RetryLimitExceededException ex)
            {
                throw new CustomRetryLimitExceededException("There is a problem with SQL Server", ex);
            }
            catch(DbUpdateException ex)
            {
                throw new CustomDbUpdateException("An error occured updating the database", ex);
            }
            catch(Exception ex)
            {
                throw new CustomException("An error occured updating the database", ex);
            }
        }

        public virtual DbSet<AddressLocation> AddressLocations { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Cashbox> Cashboxes { get; set; }
        public virtual DbSet<CashboxTransaction> CashboxTransactions { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CellProduct> CellProducts { get; set; }
        public virtual DbSet<CheckAndDisposeJob> CheckAndDisposeJobs { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DeliveryStatus> DeliveryStatuses { get; set; }
        public virtual DbSet<Deliveryman> Deliverymen { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DisposePackage> DisposePackages { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<LogSession> LogSessions { get; set; }
        public virtual DbSet<Logistic> Logistics { get; set; }
        public virtual DbSet<OpenPosition> OpenPositions { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductPackage> ProductPackages { get; set; }
        public virtual DbSet<Proffesion> Proffesions { get; set; }
        public virtual DbSet<Shipping> Shippings { get; set; }
        public virtual DbSet<SpecialCare> SpecialCares { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<TransactionProduct> TransactionProducts { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WarehouseJob> WarehouseJobs { get; set; }
        public virtual DbSet<WarehouseToDepartmentJob> WarehouseToDepartmentJobs { get; set; }
        public virtual DbSet<WishList> WishLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.Type).IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__branch__location__70DDC3D8");
            });

            modelBuilder.Entity<Cashbox>(entity =>
            {
                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Cashboxes)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__cashbox__branch___71D1E811");
            });

            modelBuilder.Entity<CashboxTransaction>(entity =>
            {
                entity.Property(e => e.Date)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Cashbox)
                    .WithMany(p => p.CashboxTransactions)
                    .HasForeignKey(d => d.CashboxId)
                    .HasConstraintName("FK__cashbox_t__cashb__73BA3083");

                entity.HasOne(d => d.CashierNavigation)
                    .WithMany(p => p.CashboxTransactions)
                    .HasForeignKey(d => d.Cashier)
                    .HasConstraintName("FK__cashbox_t__cashi__72C60C4A");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__category__depart__74AE54BC");
            });

            modelBuilder.Entity<CellProduct>(entity =>
            {
                entity.HasKey(e => new { e.BranchId, e.ProductId })
                    .HasName("PK__cell_pro__A12E100189F40DA8");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.CellProducts)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cell_prod__branc__75A278F5");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CellProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cell_prod__produ__76969D2E");
            });

            modelBuilder.Entity<CheckAndDisposeJob>(entity =>
            {
                entity.HasKey(e => e.JobsId)
                    .HasName("PK__checkAnd__2696017DBF4B6AA8");

                entity.Property(e => e.JobsId).ValueGeneratedNever();

                entity.HasOne(d => d.Jobs)
                    .WithOne(p => p.CheckAndDisposeJob)
                    .HasForeignKey<CheckAndDisposeJob>(d => d.JobsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__checkAndD__jobs___778AC167");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__customer__B9BE370FDA7C730B");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.PhoneNumber)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__customer__addres__797309D9");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__customer__user_i__787EE5A0");
            });

            modelBuilder.Entity<DeliveryStatus>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Deliveryman>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__delivery__C52E0BA83EB0395A");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.Deliveryman)
                    .HasForeignKey<Deliveryman>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__deliverym__emplo__7A672E12");
            });

            modelBuilder.Entity<DisposePackage>(entity =>
            {
                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.DisposePackages)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__dispose_p__branc__7C4F7684");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.DisposePackages)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__dispose_p__produ__7B5B524B");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.PhoneNumber)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__employee__addres__7E37BEF6");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__employee__branch__01142BA1");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__employee__depart__00200768");

                entity.HasOne(d => d.Profession)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ProfessionId)
                    .HasConstraintName("FK__employee__profes__7F2BE32F");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK__employee__users___7D439ABD");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__jobs__employee_i__02084FDA");
            });

            modelBuilder.Entity<LogSession>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.LogSessions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__log_sessi__user___02FC7413");
            });

            modelBuilder.Entity<Logistic>(entity =>
            {
                entity.HasOne(d => d.DestinationBranch)
                    .WithMany(p => p.LogisticDestinationBranches)
                    .HasForeignKey(d => d.DestinationBranchId)
                    .HasConstraintName("FK__logistics__desti__03F0984C");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Logistics)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__logistics__produ__05D8E0BE");

                entity.HasOne(d => d.StartingBranch)
                    .WithMany(p => p.LogisticStartingBranches)
                    .HasForeignKey(d => d.StartingBranchId)
                    .HasConstraintName("FK__logistics__start__04E4BC85");
            });

            modelBuilder.Entity<OpenPosition>(entity =>
            {
                entity.HasOne(d => d.Position)
                    .WithMany(p => p.OpenPositions)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK__open_posi__posit__06CD04F7");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderDescription).IsUnicode(false);

                entity.Property(e => e.PeymentStatus).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__order__branch_id__0B91BA14");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__order__customer___07C12930");

                entity.HasOne(d => d.DeliveryMan)
                    .WithMany(p => p.OrderDeliveryMen)
                    .HasForeignKey(d => d.DeliveryManId)
                    .HasConstraintName("FK__order__delivery___0A9D95DB");

                entity.HasOne(d => d.DeliveryStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryStatusId)
                    .HasConstraintName("FK__order__delivery___08B54D69");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStatusId)
                    .HasConstraintName("FK__order__order_sta__09A971A2");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK__order_pr__022945F643A30D72");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order_pro__order__0C85DE4D");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__order_pro__produ__0D7A0286");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__product__categor__0F624AF8");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK__product__supplie__0E6E26BF");
            });

            modelBuilder.Entity<ProductPackage>(entity =>
            {
                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ProductPackages)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__product_p__branc__10566F31");

                entity.HasOne(d => d.Prod)
                    .WithMany(p => p.ProductPackages)
                    .HasForeignKey(d => d.ProdId)
                    .HasConstraintName("FK__product_p__prod___114A936A");
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.HasOne(d => d.DestinationBranch)
                    .WithMany(p => p.Shippings)
                    .HasForeignKey(d => d.DestinationBranchId)
                    .HasConstraintName("FK__shipping__destin__1332DBDC");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Shippings)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__shipping__produc__123EB7A3");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Shippings)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK__shipping__suppli__14270015");
            });

            modelBuilder.Entity<SpecialCare>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__special___47027DF575F7D6EE");

                entity.Property(e => e.ProductId).ValueGeneratedNever();

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.SpecialCare)
                    .HasForeignKey<SpecialCare>(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__special_c__produ__151B244E");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__supplier__locati__160F4887");
            });

            modelBuilder.Entity<TransactionProduct>(entity =>
            {
                entity.HasKey(e => new { e.TransactionId, e.ProductId })
                    .HasName("PK__transact__C1B62770E1A34510");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TransactionProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__transacti__produ__17F790F9");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.TransactionProducts)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__transacti__trans__17036CC0");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Passwd)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<WarehouseJob>(entity =>
            {
                entity.HasKey(e => e.JobsId)
                    .HasName("PK__warehous__2696017D04CBC392");

                entity.Property(e => e.JobsId).ValueGeneratedNever();

                entity.HasOne(d => d.Jobs)
                    .WithOne(p => p.WarehouseJob)
                    .HasForeignKey<WarehouseJob>(d => d.JobsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__warehouse__jobs___18EBB532");

                entity.HasOne(d => d.Logistics)
                    .WithMany(p => p.WarehouseJobs)
                    .HasForeignKey(d => d.LogisticsId)
                    .HasConstraintName("FK__warehouse__logis__1AD3FDA4");

                entity.HasOne(d => d.Shipping)
                    .WithMany(p => p.WarehouseJobs)
                    .HasForeignKey(d => d.ShippingId)
                    .HasConstraintName("FK__warehouse__shipp__19DFD96B");
            });

            modelBuilder.Entity<WarehouseToDepartmentJob>(entity =>
            {
                entity.HasKey(e => e.JobsId)
                    .HasName("PK__warehous__2696017DDED06737");

                entity.Property(e => e.JobsId).ValueGeneratedNever();

                entity.HasOne(d => d.Jobs)
                    .WithOne(p => p.WarehouseToDepartmentJob)
                    .HasForeignKey<WarehouseToDepartmentJob>(d => d.JobsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__warehouse__jobs___1BC821DD");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WarehouseToDepartmentJobs)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__warehouse__produ__1CBC4616");
            });

            modelBuilder.Entity<WishList>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CustomerId })
                    .HasName("PK__wish_lis__0BD4214D53CE60A9");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__wish_list__custo__1EA48E88");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__wish_list__produ__1DB06A4F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
