using System;
namespace OLA.API.Models.response.Administrator
{
	public class PermissionAccessResponse
	{

        public string Module { get; set; }
        public string Role { get; set; }
        public List<string> Paths { get; set; }

        public PermissionAccessResponse(string module, string role)
        {
            this.Module = module;
            this.Role = role;
            this.Paths = new List<string>();
        }
    }
}

