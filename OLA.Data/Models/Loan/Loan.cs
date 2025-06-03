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
    public class Loan : BaseEntity
    {   
        [Required]
        public string LoanNumber { get; set; }
        public virtual AppUser User { get; set; }

        [Required]
        public DateTime DisbursementDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrincipalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal InterestRate { get; set; }

        [Required]
        public int TermsInDays { get; set; }

        [Required]

        public LoanStatus LoanStatus { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}
