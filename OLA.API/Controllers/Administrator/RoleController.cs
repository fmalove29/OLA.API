using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLA.Business.Service.Auth;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IAuthService _authService;
        public RoleController(IAuthService authService)
        {
            _authService = authService;
        }

        [Authorize]
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] AssignRoleRequest roleRequest)
        {
            var existRole = await _authService.CheckRoleExist(roleRequest.Role);

            var existUser = await _authService.GetByIdAsync(roleRequest.AppUserId);

            if (existUser == null)
            {
                return NotFound("User does not exist");
            }

            if (existRole == null)
            {
                return NotFound("Role not exist");
            }

            await _authService.AssignRole(existUser, roleRequest.Role);

            return Ok(existUser);
        }

        [Authorize]
        [HttpDelete("RemoveRole")]
        public async Task<IActionResult> Remove([FromBody] AssignRoleRequest assignRoleRequest)
        {
            var checkUser = await _authService.GetByIdAsync(assignRoleRequest.AppUserId);

            if (checkUser == null)
            {
                return NotFound("User does not exist");
            }

            var userIsInRole = await _authService.IsInRole(checkUser, assignRoleRequest.Role);

            if (userIsInRole)
            {
                await _authService.RemoveRole(checkUser, assignRoleRequest.Role);
            }

            return Ok(checkUser);
        }
    }
}
