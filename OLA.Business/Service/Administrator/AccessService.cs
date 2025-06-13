using System;
using Microsoft.EntityFrameworkCore;
using OLA.Data.Models.Administrator;
using OLA.Data.Repository;
using System.Linq.Expressions;

namespace OLA.Business.Service.Administrator
{
	public class AccessService : IAccessService
	{
		private readonly IRepository<Access> _repostory;
		public AccessService(IRepository<Access> repository)
		{
			_repostory = repository;
		}

		public async Task<DbSet<Access>> GetDbSet()
		{
			return await _repostory.GetDbSet();
		}

		public async Task<IEnumerable<Access>> GetAll()
		{
			var accesses = (await _repostory.GetDbSet());
			return accesses;
		}
		public async Task Add(Access access)
		{
			await _repostory.Add(access);
		}

		public async Task Update(Access access)
		{
			await _repostory.Update(access);
		}

		public async Task<Access> GetById(Guid Id)
		{
			var acc = (await _repostory.GetDbSet())
					  .Where(acc => acc.Id == Id)
					  .FirstOrDefault();
			return acc;
		}

		public async Task Delete(Access access)
		{
			await _repostory.Delete(access);
		}

		public async Task SaveChangesAsync(Guid Id)
		{
			await _repostory.SaveChangesAsync(Id);
		}

		public async Task<IEnumerable<Access>> GetByCondition(Expression<Func<Access, bool>> whereExp)
		=> await (await _repostory.GetDbSet())
				.Where(whereExp)
				.ToListAsync();
	}
}

