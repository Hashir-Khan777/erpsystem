using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ZiniTechERPSystem.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor HttpContextAccessor) : IdentityDbContext<ApplicationUser>(options)
    {
        private IHttpContextAccessor HttpContextAccessor = HttpContextAccessor;
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ProductInvoice> ProductInvoice { get; set; }
        public DbSet<EmployeeTask> Tasks { get; set; }
        public DbSet<AuditLog> Logs { get; set; }

        private void AuditChanges()
        {
            var UserId = HttpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var UserClaim = HttpContextAccessor.HttpContext?.User?.Claims?.ToList();

            if (UserClaim == null) return;

            var UserRole = "";

            try
            {
                UserRole = UserClaim[4]?.Value;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                UserRole = null;
            }


            var entries = ChangeTracker.Entries()
                                        .Where(e => e.State == EntityState.Added
                                                 || e.State == EntityState.Modified
                                                 || e.State == EntityState.Deleted).ToList();

            foreach (var entry in entries)
            {
                var entityId = entry.Property("Id").CurrentValue;
                var entityTableName = entry.Metadata.GetTableName();
                var sql = $"SELECT Value FROM (SELECT Id, CAST (ROW_NUMBER() OVER(ORDER BY Id) AS int) AS Value FROM {entityTableName}) AS d WHERE d.Id = '{entityId}'";
                var rowNumber = this.Database.SqlQueryRaw<int>(sql).FirstOrDefault();
                var auditEntry = new AuditLog
                {
                    UserId = UserId,
                    UserRole = UserRole,
                    EntityName = entry.Entity.GetType().Name,
                    ActionType = entry.State.ToString(),
                    RowNumber = rowNumber,
                    Timestamp = DateTime.UtcNow
                };

                Logs.Add(auditEntry);
            }
        }

        public override int SaveChanges()
        {
            AuditChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AuditChanges();   
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Company>()
                .HasOne(c => c.Manager)
                .WithMany(m => m.Companies)
                .HasForeignKey(m => m.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Company>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Company>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Company)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ApplicationUser>()
                .HasMany(c => c.Products)
                .WithOne(p => p.CreatedBy)
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Company>()
                .HasMany(c => c.Customers)
                .WithOne(p => p.Company)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ApplicationUser>()
                .HasMany(c => c.Customers)
                .WithOne(p => p.CreatedBy)
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Company>()
                .HasMany(c => c.Invoices)
                .WithOne(i => i.Company)
                .HasForeignKey(i => i.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ApplicationUser>()
                .HasMany(c => c.Invoices)
                .WithOne(i => i.CreatedBy)
                .HasForeignKey(i => i.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Customer>()
                .HasMany(c => c.Invoices)
                .WithOne(i => i.Customer)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ProductInvoice>()
                .HasKey(pc => new { pc.ProductId, pc.InvoiceId });

            builder.Entity<ProductInvoice>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductInvoices)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ProductInvoice>()
                .HasOne(pc => pc.Invoice)
                .WithMany(p => p.ProductInvoices)
                .HasForeignKey(pc => pc.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ApplicationUser>()
                .HasMany(c => c.AssignedTasks)
                .WithOne(p => p.Asignee)
                .HasForeignKey(p => p.AsigneeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ApplicationUser>()
                .HasMany(c => c.Logs)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
