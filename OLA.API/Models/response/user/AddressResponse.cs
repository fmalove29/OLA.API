using System;
namespace OLA.API.Models.response.user
{
	public class AddressResponse : BaseResponse
	{
		public string AppUserId { get; set; }
		public string Barangay { get; set; }
		public string Purok { get; set; }
		public string City { get; set; }
	}
}

