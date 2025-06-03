using System;
namespace OLA.API.Models.request.Loan
{
	public class LoanApplicationRequest
	{
        public Guid AppUserId { get; set; }
        public decimal AmountRequested { get; set; }
        public int TermsInDays { get; set; }
        public string Purpose { get; set; }
        public string Notes { get; set; }
    }
}

