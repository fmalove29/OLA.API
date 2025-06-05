using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;
using OLA.API.Models.request.User;
using OLA.Business.Service.Auth;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using OLA.Business.Service.Account;
using OLA.Data.Models.User;
using OLA.API.Models.response.user;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AddressService _addressService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
            _addressService = _addressService;
        }

        [Authorize]
        [HttpPut("ProfileUpdate")]
        public async Task<IActionResult> UpdateProfile([FromBody] AppUserRequest appUserRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var objectId = await _authService.GetUserIdByEmail(email);

            try
            {
                var findUser = await _authService.GetDbSet()
                                 .Include(f => f.Families)
                                 .Include(a => a.Addresses)
                                 .FirstOrDefaultAsync(e => e.Id == objectId);

                if (findUser == null)
                    return NotFound("User not found or Not Authenticated");


                findUser.Email = appUserRequest.Email;
                findUser.PhoneNumber = appUserRequest.PhoneNumber;

                
                await _authService.Update(findUser);

                foreach (var userAddress in findUser.Addresses)
                {
                    var existAddress = (await _addressService.GetDbSet())
                                       .FirstOrDefaultAsync(a => a.AppUserId == objectId)
                                       .Select(a => new AddressResponse
                                       {
                                           Barangay = a.Barangay,
                                           City = a.City,
                                           Purok = a.Purok
                                       });
                    return Ok(existAddress);
                   
                    //if (existAddress == null)
                    //{
                    //    var newAddress = new OLA.Data.Models.User.Address
                    //    {
                    //        Barangay = userAddress.Barangay,
                    //        Purok = userAddress.Purok,
                    //        City = userAddress.City
                    //    };

                    //    await _addressService.Add(newAddress);
                    //    await _addressService.SaveChangesAsync(Guid.Parse(objectId));
                    //}
                    //else
                    //{
                    //    existAddress.Barangay = userAddress.Barangay;
                    //    existAddress.City = userAddress.City;
                    //    existAddress.Purok = userAddress.Purok;
                    //}
              
                }
            }
            catch (Exception ex)
            {

            }
            return Ok(appUserRequest);
        }
    }
}
