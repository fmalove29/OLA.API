using System;
using Microsoft.AspNetCore.Identity;
using OLA.Data.Models.Loan;

namespace OLA.Data.Models.User
{
	public class AppUser : IdentityUser
	{
		public string FirstName {get; set;}
		public string LastName {get; set;}
		public string? MiddleName {get; set;}
		public virtual ICollection<Family> Families {get; set;}
		public virtual ICollection<Address> Addresses {get; set;}
		public virtual ICollection<Payment> Payments { get; set; }
		public virtual ICollection<OLA.Data.Models.Loan.Loan> Loans { get; set; }
		public virtual ICollection<LoanApplication> LoanApplications { get; set; }
	}
}

