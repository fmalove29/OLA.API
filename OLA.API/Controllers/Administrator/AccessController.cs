using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLA.Business.Service.Administrator;
using Microsoft.AspNetCore.Authorization;
using OLA.API.Models.Filter;
using OLA.API.Extension.Administrator;
using OLA.API.Models.request.Administrator;
using OLA.Business.Service.Auth;
using System.Security.Claims;
using Microsoft.Identity.Client;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IAccessService _accessService;
        private readonly IAuthService _authService;

        public AccessController(IAccessService accessService, IAuthService authService)
        {
            _accessService = accessService;
            _authService = authService;

        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AccessFilter filter)
        {
            var accesses = await _accessService.GetAll();

            var filtered = accesses
                .Where(a =>
                        (!string.IsNullOrEmpty(filter.Search) ? a.Name.Contains(filter.Search, StringComparison.OrdinalIgnoreCase) : true)
                        && (!string.IsNullOrEmpty(filter.Module) ? a.Module.Contains(filter.Module, StringComparison.OrdinalIgnoreCase) : true)
                        && (!string.IsNullOrEmpty(filter.Role) ? a.Roles.Contains(filter.Role, StringComparison.OrdinalIgnoreCase) : true)
                )
                .ToList()
                .OrderBy(a => a.Name)
                .ToPagedList(filter.Page, filter.Limit);

            return Ok(filtered); // Or Ok(filtered) if not using custom HrisOk()
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AccessRequest accessRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var objectId = await _authService.GetUserIdByEmail(email);
            try
            {
                var newAccess = new OLA.Data.Models.Administrator.Access
                {
                    Name = accessRequest.Name,
                    Path = accessRequest.Path,
                    Module = accessRequest.Module,
                    Roles = string.Join(",", accessRequest.Roles)
                };
                await _accessService.Add(newAccess);
                await _accessService.SaveChangesAsync(Guid.Parse(objectId));

                return Ok(newAccess);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
