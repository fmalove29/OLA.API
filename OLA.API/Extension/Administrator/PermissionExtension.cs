using System;
using OLA.API.Models.response.Administrator;

namespace OLA.API.Extension.Administrator
{
	public static class PermissionExtension
	{
		public static List<PermissionAccessResponse> ToPermissionAccessListResponse(this List<string> access)
		{
			var response = new List<PermissionAccessResponse>();

			foreach (var x in access) 
            { 
                var splitted = x.Split('.', StringSplitOptions.RemoveEmptyEntries);
                response.Add(new PermissionAccessResponse(splitted[0], splitted[1]));
            }
            return response;
		}
	}
}

