using System;
using OLA.API.Models.response.loan;
using OLA.Data.Models.Loan;

namespace OLA.API.Extension.Loan
{
	public static class LoanApplicationExtension
	{
		public static LoanApplicationResponse ToLoanApplicationResponse(this LoanApplication loanApplication)
		=> new LoanApplicationResponse
		{
			Id = loanApplication.Id,
			TermsInDays = loanApplication.TermsInDays,
			AmountRequested = loanApplication.AmountRequested,
			Notes = loanApplication.Notes,
			Purpose = loanApplication.Purpose
		};
	}
}

