using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OLA.Data.Models;
using OLA.Data.Models.Administrator;
using OLA.Data.Models.Loan;
using OLA.Data.Models.User;

namespace OLA.Data.DataContext
{
	public class ApplicationDbContext : IdentityDbContext<AppUser>, IApplicationDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

        public async Task<int> SaveChangesAsync(Guid userObjectId)
        {
            return await SaveChanges(userObjectId);
        }
        public async Task<int> SaveChanges(Guid userObjectId)
        {
            var selectedEntityList = ChangeTracker.Entries().
                Where(x => x.Entity is BaseEntity && (x.State == EntityState.Modified || x.State == EntityState.Added));


            foreach (var entity in selectedEntityList)
            {
                ((BaseEntity)entity.Entity).ModifiedBy = userObjectId;
                ((BaseEntity)entity.Entity).Modified = DateTime.Now.ToUniversalTime();

                if (entity.State == EntityState.Added)
                    ((BaseEntity)entity.Entity).Id = Guid.NewGuid();
            }

            return await base.SaveChangesAsync();
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Access> Accesses { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Permission>().HasKey(e => e.Id);


            modelBuilder.Entity<Access>().HasData(
                //Security
                new { Id = Guid.NewGuid(), Active = true, Name = "User", Path = "User",   Module = "Security", Roles= "Admin", Modified = DateTime.UtcNow.ToLocalTime(), ModifiedBy = Guid.NewGuid()},
                new { Id = Guid.NewGuid(), Active = true, Name = "Role", Path = "Role", Module = "Security", Roles = "Admin", Modified = DateTime.UtcNow.ToLocalTime(), ModifiedBy = Guid.NewGuid() },
                new { Id = Guid.NewGuid(), Active = true, Name = "Access", Path ="Access",  Module = "Security", Roles = "Admin", Modified = DateTime.UtcNow.ToLocalTime(), ModifiedBy = Guid.NewGuid() },
                new { Id = Guid.NewGuid(), Active = true, Name = "Permission", Path = "Permission", Module = "Security", Roles = "Admin", Modified = DateTime.UtcNow.ToLocalTime(), ModifiedBy = Guid.NewGuid() },

                //Customer
                new { Id = Guid.NewGuid(), Active = true, Name = "Customer Enrollment", Path = "Enrollment", Module = "Customer", Roles = "Admin,User", Modified = DateTime.UtcNow.ToLocalTime(), ModifiedBy = Guid.NewGuid() },

                //Loan
                new { Id = Guid.NewGuid(), Active = true, Name = "Loan Application", Path  = "LoanApplication", Module = "Loan", Roles = "Admin,User", Modified = DateTime.UtcNow.ToLocalTime(), ModifiedBy = Guid.NewGuid() },
                new { Id = Guid.NewGuid(), Active = true, Name = "Loan", Module = "Loan", Path = "Loan", Roles = "Admin,User", Modified = DateTime.UtcNow.ToLocalTime(), ModifiedBy = Guid.NewGuid() }

                );
        }

    }



}

