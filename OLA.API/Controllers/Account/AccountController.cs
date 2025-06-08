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
        private readonly IAddressService _addressService;
        public AccountController(IAuthService authService, IAddressService addressService)
        {
            _authService = authService;
            _addressService = addressService;
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


                findUser.PhoneNumber = appUserRequest.PhoneNumber;


                await _authService.Update(findUser);



                foreach (var d in appUserRequest.Addresses)
                {
                    var existAddress = await (await _addressService.GetDbSet())
                                        .FirstOrDefaultAsync(a => a.AppUserId == objectId && a.Id == d.Id);

                    if (existAddress == null)
                    {
                        var newAddress = new OLA.Data.Models.User.Address
                        {
                            Barangay = d.Barangay,
                            Purok = d.Purok,
                            City = d.City
                        };

                        await _addressService.Add(newAddress);
                        await _addressService.SaveChangesAsync(Guid.Parse(objectId));
                    }
                    else
                    {
                        existAddress.Barangay = d.Barangay;
                        existAddress.City = d.City;
                        existAddress.Purok = d.Purok;

                        await _addressService.Update(existAddress);
                        await _addressService.SaveChangesAsync(Guid.Parse(objectId));
                        
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return Ok(appUserRequest);
        }
    }
}
