using OLA.Data.Models.Enum;
using OLA.Data.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLA.Data.Models.Loan
{
    public class LoanApplication : BaseEntity
    {


        [Required]
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountRequested { get; set; }
        [Required]
        public int TermsInDays { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal InterestRate { get; set; } = 0.20m;

        [Required]
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow.ToLocalTime();

        [Required]
        public LoanApplicationStatus ApplicationStatus { get; set; }

        public string ApplicationStatusName => ApplicationStatus.ToString();

        public DateTime? ApprovalDate  { get; set; }
        public Guid? ApprovedBy { get; set; }
        public string? Purpose { get; set; }
        public string? Notes { get; set; }
        public DateTime? DisbursementDate { get; set; }
    }
}
