using System;
using Microsoft.EntityFrameworkCore;
using OLA.Data.Models.User;

namespace OLA.Business.Service.Account
{
	public interface IFamilyService
	{
        Task<Family> GetByIdAsync(Guid Id);
        Task Add(Family family);
        Task Update(Family family);
        Task<DbSet<Family>> GetDbSet();
        Task<bool> SaveChangesAsync(Guid Id);
    }
}

