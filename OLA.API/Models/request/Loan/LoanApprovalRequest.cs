using System;
namespace OLA.API.Models.request.Loan
{
	public class LoanApprovalRequest : LoanApplicationRequest
	{
        public DateTime? DisbursementDate { get; set; }
    }
}

