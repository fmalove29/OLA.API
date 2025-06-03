using System.ComponentModel.DataAnnotations.Schema;
using OLA.Data.Models.Enum;
using OLA.Data.Models.User;
namespace OLA.Data.Models.Loan
{
	public class Payment : BaseEntity
	{
		public virtual LoanApplication  LoanApplication {get; set;}
		public virtual Loan Loan { get; set; }
		public DateTime PaymentDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
		public decimal AmountPaid {get; set;}

		public PaymentMethod PaymentMethod { get; set; }

    }
}

