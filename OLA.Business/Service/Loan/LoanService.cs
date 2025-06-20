using System;
using OLA.Data.Repository;


namespace OLA.Business.Service.Loan
{
	public class LoanService
	{
		private readonly IRepository<OLA.Data.Models.Loan.Loan> _repository;
		public LoanService(IRepository<OLA.Data.Models.Loan.Loan> repository)
		{
			_repository = repository;
		}

		public async Task Add(OLA.Data.Models.Loan.Loan loan)
		{
			await _repository.Add(loan);
		}

        public Task<string> GenerateLoanNumber()
        {
            var prefix = "LN";
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var random = Guid.NewGuid().ToString().Substring(0, 4).ToUpper(); // 4 random characters

            var loanNumber = $"{prefix}-{timestamp}-{random}";

            return Task.FromResult(loanNumber);
        }

        public  Task<DateTime> GetDueDate(DateTime approvalDate, int termsInDays)
        {
            var due = approvalDate.AddDays(termsInDays);

            return Task.FromResult(due.ToLocalTime());
        }

        public async Task<bool> SaveChangesAsync(Guid Id)
        {
            return await _repository.SaveChangesAsync(Id);
        }
    }
}

