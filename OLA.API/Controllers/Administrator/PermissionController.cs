using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OLA.Business.Service.Administrator;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;
using OLA.Business.Service.Auth;
using OLA.API.Extension.Administrator;
using OLA.API.Models.request.Administrator;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly IAccessService _accessService;
        private readonly IAuthService _authService;
        public PermissionController(IPermissionService permissionService, IAuthService authService, IAccessService accessService)
        {
            _permissionService = permissionService;
            _authService = authService;
            _accessService = accessService;
        }

        [HttpGet("access")]
        public async Task<IActionResult> GetUserAccess()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var objectId = await _authService.GetUserIdByEmail(email);

            var authUser = await _authService.GetByIdAsync(objectId);

            if (authUser == null)
            {
                return NotFound("User not found");
            }

            if (authUser.Id == null)
            {
                return NotFound("UserId is not found");
            }

            var permissionAccess = (await _permissionService.GetPermissionAsync(objectId))
                       .ToPermissionAccessListResponse();

            

            if (permissionAccess == null)
                return NotFound("No Permission(s) found.");

            for (int i = 0; i < permissionAccess.Count; i++)
            {
                var access = await _accessService.GetByCondition(x => x.Module.Equals(permissionAccess[i].Module) && x.Roles.Contains(permissionAccess[i].Role));

                if (access != null && access.Any())
                    foreach (var a in access)
                        permissionAccess[i].Paths.Add(a.Path);
            }
            return Ok(permissionAccess);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PermissionRequest permissionRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var objectId = await _authService.GetUserIdByEmail(email);


            try
            {
                var appUser = await _authService.GetDbSet()
                                .Include(f => f.Families)
                                .Include(a => a.Addresses)
                                .FirstOrDefaultAsync(e => e.Id == permissionRequest.AppUserId);
                if (appUser == null)
                {
                    return NotFound("User not Found");
                }

                await _permissionService.ManagePermission(objectId, appUser, permissionRequest.Modules, permissionRequest.Role);
                await _permissionService.SaveChangesAsync(Guid.Parse(objectId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(permissionRequest);
        }
    }
}
