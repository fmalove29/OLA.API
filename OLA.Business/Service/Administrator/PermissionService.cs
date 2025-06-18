using System;
using OLA.Business.Service.Auth;
using OLA.Data.Models.Administrator;
using OLA.Data.Repository;
using Microsoft.EntityFrameworkCore;
using OLA.Data.Models.User;
using System.Collections.Generic;
using System.Security.Claims;

namespace OLA.Business.Service.Administrator
{
	public class PermissionService : IPermissionService
	{
		private readonly IRepository<Permission> _permission;
		private readonly IAuthService _authService;
		public PermissionService(IRepository<Permission> permission, IAuthService authService)
		{
			_permission = permission;
			_authService = authService;
		}

		public async Task<List<string>> GetPermissionAsync(string appUserId)
		{
			var permissions = new List<string>();

			var appUser = await _authService.GetByIdAsync(appUserId);

			if(appUser != null)
			{
				var user = await _permission.GetDbSet().Result.Where(e => e.AppUserId == appUser.Id).FirstOrDefaultAsync();

				if(user != null)
				{
					if(!string.IsNullOrEmpty(user.Access))
					{
						permissions.AddRange(user.Access.Split(',', StringSplitOptions.RemoveEmptyEntries));
					}
				}
			}
			return permissions;
		}

		public async Task ManagePermission(string appUserId, AppUser appUser, string module, string role)
		{
			var permission = await _permission.FindByConditionAsync(e => e.AppUserId == appUser.Id);

			var newPermission = $"{module}.{role}";



            if (permission != null)
			{
				var existingPermissions = await this.GetPermissionAsync(appUserId);

				if (existingPermissions.Contains(newPermission))
				{
					throw new Exception("Permission already exist.");
				}

				if (role == "admin" || role == "Admin")
				{
					if (existingPermissions.Contains($"{module}.User"))
						existingPermissions.Remove(module + ".User");
				}

				existingPermissions.Add(newPermission);
				permission.Access = string.Join(",", existingPermissions);
				await _permission.Update(permission);
			}
			else
			{
				await _permission.Add(new Permission()
				{
					AppUserId = appUser.Id,
					Access = newPermission
				});
			}
        }


		public async Task<bool> HasRights(string module, string role, string userId)
		{
			var permission = await this.GetPermissionAsync(userId);

			if (permission.Count > 0)
			{
				var rights = permission.Where(e => e.Contains(module, StringComparison.CurrentCultureIgnoreCase)).ToList();

				if (rights != null && rights.Any())
				{
					var roles = rights.Where(e => e.Trim().Contains($"{module}.{role}", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                    return !string.IsNullOrEmpty(roles);
                }
			}

			return false;
		}

        public async Task<List<string>> GetPermissions(string id)
        {
            var permissions = new List<string>();
            var employee = await _permission.GetDbSet().Result.Where(e => e.AppUserId == id).FirstOrDefaultAsync();

            if (employee != null)
                if (!string.IsNullOrEmpty(employee.Access))
                    permissions.AddRange(employee.Access.Split(',', StringSplitOptions.RemoveEmptyEntries));
            return permissions;
        }

        public async Task<bool> SaveChangesAsync(Guid Id)
        {
			return await _permission.SaveChangesAsync(Id);
        }
    }
}

