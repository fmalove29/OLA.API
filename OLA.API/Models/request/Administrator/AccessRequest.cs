using System;
namespace OLA.API.Models.request.Administrator
{
	public class AccessRequest : BaseRequest
	{
        public string Name { get; set; }
        public string Path { get; set; }
        public string Module { get; set; }
        public string Roles { get; set; }
    }
}

