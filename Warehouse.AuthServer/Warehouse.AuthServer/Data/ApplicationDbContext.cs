using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Warehouse.AuthServer.Models;

namespace Warehouse.AuthServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationRole>().Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<ApplicationRole>().Property(e => e.NormalizedName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<ApplicationUser>().Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<ApplicationUser>().Property(e => e.NormalizedUserName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<ApplicationUser>().Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<ApplicationUser>().Property(e => e.NormalizedEmail)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<ApplicationUser>().Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Entity<ApplicationUser>().Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<IdentityUserRole<Guid>>(
                    au => au.HasOne<ApplicationRole>()
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(u => u.RoleId),
                    au => au.HasOne<ApplicationUser>()
                        .WithMany(u => u.UserRoles)
                        .HasForeignKey(r => r.UserId));
        }

    }
}
