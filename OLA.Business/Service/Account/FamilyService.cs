using System;
using Microsoft.EntityFrameworkCore;
using OLA.Data.Models.User;
using OLA.Data.Repository;

namespace OLA.Business.Service.Account
{
	public class FamilyService : IFamilyService
	{
        private readonly IRepository<Family> _repository;

        public FamilyService(IRepository<Family> repository)
        {
            _repository = repository;
        }
        public async Task Add(Family family)
        {
            await _repository.Add(family);
        }

        public async Task<Family> GetByIdAsync(Guid Id)
        {
            var fam = await _repository.GetByIdAsync(Id);
            return fam;
        }

        public async Task<DbSet<Family>> GetDbSet()
        {
            return await _repository.GetDbSet();
        }

        public async Task<bool> SaveChangesAsync(Guid Id)
        {
            return await _repository.SaveChangesAsync(Id);
        }

        public async Task Update(Family address)
        {
            await _repository.Update(address);
        }
    }
}

