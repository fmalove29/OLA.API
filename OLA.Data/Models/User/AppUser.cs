using System;
using Microsoft.AspNetCore.Identity;

namespace OLA.Data.Models.User
{
	public class AppUser : IdentityUser
	{
		public string FirstName {get; set;}
		public string LastName {get; set;}
		public string? MiddleName {get; set;}
		public virtual ICollection<Family> Families {get; set;}
		public virtual ICollection<Address> Addresses {get; set;}
	}
}

