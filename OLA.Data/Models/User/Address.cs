using System;
namespace OLA.Data.Models.User
{
	public class Address : BaseEntity
	{
		public string Barangay {get; set;}
		public string City {get; set;}
		public string Purok {get; set;}
	}
}

