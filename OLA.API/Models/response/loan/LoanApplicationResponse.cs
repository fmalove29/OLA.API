using System;
using OLA.API.Models.response.user;
namespace OLA.API.Models.response.loan
{
    public class LoanApplicationResponse : BaseResponse
    {
        public virtual AppUserResponse AppUser {get; set;}
        public decimal AmountRequested { get; set; }
        public int TermsInDays { get; set; }
        public string Purpose { get; set; }
        public string Notes { get; set; }

    }
}

