using OLA.Data.Models.Enum;
using OLA.Data.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLA.Data.Models.Loan
{
    public class LoanApplication : BaseEntity
    {
        public virtual AppUser AppUser { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountRequested { get; set; }

        public int TermsInDays { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal InterestRate { get; set; }

        public DateTime ApplicationDate { get; set; }

        public LoanApplicationStatus ApplicationStatus { get; set; }

        public DateTime? ApprovalDate  { get; set; }
        public Guid? ApprovedBy { get; set; }
        public string Purpose { get; set; }
        public string Notes { get; set; }
        public DateTime? DisbursementDate { get; set; }
    }
}
