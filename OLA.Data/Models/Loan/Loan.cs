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
    public class Loan : BaseEntity
    {
        public string LoanNumber { get; set; }
        public virtual AppUser User { get; set; }

        public DateTime DisbursementDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrincipalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal InterestRate { get; set; }
        public int TermsInDays { get; set; }

        public LoanStatus LoanStatus { get; set; }
        public required DateTime DueDate { get; set; }
    }
}
