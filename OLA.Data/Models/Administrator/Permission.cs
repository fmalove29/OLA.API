using System;
namespace OLA.Data.Models.Administrator
{
	public class Permission : BaseEntity
	{
		public string AppUserId { get; set; }
		public virtual OLA.Data.Models.User.AppUser AppUser { get; set; }

		public string Access { get; set; }
	}
}

