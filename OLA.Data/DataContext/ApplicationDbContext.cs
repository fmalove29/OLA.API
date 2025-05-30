using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OLA.Data.Models;
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
    }



}

