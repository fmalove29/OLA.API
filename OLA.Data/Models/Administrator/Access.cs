using System;
namespace OLA.Data.Models.Administrator
{
	public class Access : BaseEntity
	{
        public string Name { get; set; }
        public string Path { get; set; }
        public string Module { get; set; }
        public string Roles { get; set; }
    }
}

