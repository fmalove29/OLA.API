using System;
namespace OLA.API.Models.response.user
{
	public class AddressResponse 
	{
		public Guid Id { get; set; }
		public string AppUserId { get; set; }
		public string Barangay { get; set; }
		public string Purok { get; set; }
		public string City { get; set; }
	}
}

