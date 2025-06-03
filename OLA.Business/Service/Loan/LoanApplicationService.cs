using System;
using Microsoft.EntityFrameworkCore;
using OLA.Data.Models.Loan;
using OLA.Data.Repository;
using System.Linq;
using System.Linq.Expressions;

namespace OLA.Business.Service.Loan
{
	public class LoanApplicationService
	{
		public readonly IRepository<LoanApplication> _repository;
		public LoanApplicationService(IRepository<LoanApplication> repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<LoanApplication>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<LoanApplication> GetByIdAsync(Guid loanApplicationId)
		{
			return await _repository.GetByIdAsync(loanApplicationId);
		}

		public async Task<LoanApplication> AddAsync(LoanApplication loanApplication)
		{
			var application = await _repository.Add(loanApplication);
			return application;
		}

		public async Task<LoanApplication> UpdateAsync(LoanApplication loanApplication)
		{
			var application = await _repository.Update(loanApplication);

			return application;
		}

		public async Task<LoanApplication> DeleteAsync(LoanApplication loanApplication)
		{
			var application = await _repository.Delete(loanApplication);
			return application;
		}
        public async Task<DbSet<LoanApplication>> GetDbSet()
        => await _repository.GetDbSet();

		public async Task<IEnumerable<LoanApplication>> GetByCondition(Expression<Func<LoanApplication, bool>> whereExp)
		=> await (await _repository.GetDbSet())
			.Where(whereExp)
			.ToListAsync();

        public async Task<bool> SaveChangesAsync(Guid Id)
		{
			return await _repository.SaveChangesAsync(Id);
		}

		
	}
}

