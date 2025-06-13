using System;
namespace OLA.API.Models.Filter
{
	public class AccessFilter : BaseFilter
	{
		public string? Name { get; set; }
		public string? Module { get; set; }
		public string? Role { get; set; }
	}
}

