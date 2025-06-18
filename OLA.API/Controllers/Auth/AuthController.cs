using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using OLA.API.Models.request.User;
using OLA.Business.Service.Auth;
using OLA.Data.Models.User;
using OLA.Data.Models.Enum;
using OLA.API.Models.dto;
using Microsoft.EntityFrameworkCore;



namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }
        //[Authorize]
        [HttpPost("customer-enrollment")]
        public async Task<IActionResult> enroll([FromBody] AppUserRequest appUserRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            
            var objectId = await _authService.GetUserIdByEmail(email);


            var user = new AppUser
            {
                FirstName = appUserRequest.FirstName,
                LastName = appUserRequest.LastName,
                MiddleName = appUserRequest.MiddleName,
                UserName = appUserRequest.UserName,
                Email = appUserRequest.Email,
                PhoneNumber = appUserRequest.PhoneNumber
            };


            var createdUser =  await _authService.CreateUser(user, appUserRequest.Password);


            if(createdUser.Succeeded)
            {
                await _authService.AssignRole(user, "User");
            }

            return Ok(new { success = true, data = user });
        }


        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody]LoginDTO logUser)
        {
            var result = await _authService.Login(new AppUser { Email = logUser.Email }, logUser.Password);

            if (result == null)
                return Unauthorized(new { message = "Invalid credentials" });


            var token = await _tokenService.CreateToken(result);

            return Ok(new { userToken = token });

        }

    }
}