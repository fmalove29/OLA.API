using System;
using OLA.API.Models.request;
using OLA.API.Models.request.Administrator;

namespace OLA.API.Models.request.Administrator
{
	public class PermissionRequest : BaseRequest
	{
		public string AppUserId { get; set; }
		public string Modules { get;  set; }
        public string Role { get; set; }
    }
}
public class PermissionChangeRequest : PermissionRequest
{
    public string NewModule { get; set; }
    public string NewRole { get; set; }
}

public class AssignRoleRequest
{
    public string AppUserId { get; set; }
    public string Role { get; set; }
}
