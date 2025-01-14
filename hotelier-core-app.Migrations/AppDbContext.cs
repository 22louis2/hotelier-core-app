using hotelier_core_app.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hotelier_core_app.Migrations
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, IdentityRoleClaim<long>, ApplicationUserToken>
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleGroup> ModuleGroups { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .HasColumnType("bytea")
                .HasDefaultValueSql("gen_random_bytes(8)");

            modelBuilder.Entity<ApplicationUserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
        }
    }
}
