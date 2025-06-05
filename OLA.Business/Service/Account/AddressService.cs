using System;
using Microsoft.EntityFrameworkCore;
using OLA.Data.Models.User;
using OLA.Data.Repository;

namespace OLA.Business.Service.Account
{
	public class AddressService
	{
		private readonly IRepository<Address> _repository;
		public AddressService(IRepository<Address> repository)
		{
			_repository = repository;
		}
		public async Task<Address> GetByIdAsync(Guid Id)
		{
			var address = await _repository.GetByIdAsync(Id);
			return address;
		}

		public async Task Add(Address address)
		{
			await _repository.Add(address);
		}

		public async Task Update(Address address)
		{
			await _repository.Update(address);
		}

		public async Task<DbSet<Address>> GetDbSet()
		=> await _repository.GetDbSet();

        public async Task<bool> SaveChangesAsync(Guid Id)
        {
            return await _repository.SaveChangesAsync(Id);
        }
    }
}

