using System;
using OLA.Data.Models.Administrator;
using OLA.Data.Models.User;

namespace OLA.Business.Service.Administrator
{
	public interface IPermissionService
	{
		Task<List<string>> GetPermissionAsync(string appUserId);
		Task ManagePermission(string appUserId, AppUser appUser, string module, string role);
		Task<bool> SaveChangesAsync(Guid Id);
		Task<bool> HasRights(string module, string role, string userId);

    }
}

