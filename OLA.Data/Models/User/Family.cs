using System;
using OLA.Data.Models.Enum;

namespace OLA.Data.Models.User
{
	public class Family : BaseEntity
	{
		public string Name {get; set;}
		public string ContactNumber {get; set;}
		public RelationshipType RelationType { get; set; }
        public DateTime DateOfBrith { get; set; }
        public bool IsEmergencyContact { get; set; }
        public Guid? AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}

