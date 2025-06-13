using System;
using OLA.API.Models;
using OLA.API.Models.response.user;
namespace OLA.API.Models.response.user
{
	public class FamilyResponse : BaseResponse
	{
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public OLA.API.Models.RelationshipType RelationshipType { get; set; }
        public string RelationshipTypeName { get; set; }
        public DateTime DateOfBrith { get; set; }
        public bool IsEmergencyContact { get; set; }
        public virtual AddressResponse Address { get; set; }
    }
}

