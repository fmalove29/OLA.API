using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using OLA.API.Models.request.User;
using OLA.Business.Service.Auth;
using OLA.Data.Models.User;
using OLA.Data.Models.Enum;
using OLA.API.Models.dto;



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
        [Authorize]
        [HttpPost("customer-enrollment")]
        public async Task<IActionResult> enroll([FromBody] AppUserRequest appUserRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            // stored as user.Id by default
            // if (string.IsNullOrEmpty(userId))
            //     return null;
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


            // Save user first
            var createdUser =  await _authService.CreateUser(user, appUserRequest.Password);


            if(createdUser.Succeeded)
            {
                await _authService.AssignRole(user, "User");
            }

            // Save Addresses separately
            var addressList = appUserRequest.Addresses.Select(a => new OLA.Data.Models.User.Address
            {
                Active = a.Active,
                Barangay = a.Barangay,
                City = a.City,
                Purok = a.Purok
            }).ToList();

            if(appUserRequest.Addresses != null)
            {
                foreach(var address in addressList)
                {
                    await _authService.CreateAddress(address);
                    await _authService.SaveChangesAsync(Guid.Parse(objectId));
                }
            }
            



            // // Save Families separately
            var familyList = appUserRequest.Families.Select(f => new OLA.Data.Models.User.Family
            {
                Active = f.Active,
                Name = f.Name,
                ContactNumber = f.ContactNumber,
                RelationType = (RelationshipType)(int)f.RelationType,
                DateOfBrith = f.DateOfBrith,
                IsEmergencyContact = f.IsEmergencyContact,
                Address = new OLA.Data.Models.User.Address
                {
                    Active = f.Address.Active,
                    Barangay = f.Address.Barangay,
                    City = f.Address.City,
                    Purok = f.Address.Purok,
                }
            }).ToList();

            if(appUserRequest.Families != null)
            {
                foreach(var family in familyList)
                {
                    await _authService.AddFamilies(family);
                    await _authService.SaveChangesAsync(Guid.Parse(objectId));
                }
            }

            return Ok(new { success = true, data = user });
        }


        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody]LoginDTO logUser)
        {
            var user = new AppUser
            {
                Email = logUser.Email
            };

            var result = await _authService.Login(user, logUser.Password);

            if (result == null)
                return Unauthorized(new { message = "Invalid credentials" });

            var token = await _tokenService.CreateToken(user);

            return Ok(new {
                userToken = token
            });
        }

    }
}