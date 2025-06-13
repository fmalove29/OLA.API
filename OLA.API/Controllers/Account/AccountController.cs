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
using OLA.Data.Models.Enum;
using OLA.API.Extension.Account;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAddressService _addressService;
        private readonly IFamilyService _familyService;
        public AccountController(IAuthService authService, IAddressService addressService, IFamilyService familyService)
        {
            _authService = authService;
            _addressService = addressService;
            _familyService = familyService;
        }

        [Authorize]
        [HttpPut("ProfileUpdate")]
        public async Task<IActionResult> UpdateProfile([FromBody] AppUserRequest appUserRequest)
        {
            //return Ok(appUserRequest);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var objectId = await _authService.GetUserIdByEmail(email);


            if (appUserRequest.Families == null)
            {
                return BadRequest("Add at leat 1 Family member");
            }
            if (appUserRequest.Addresses == null)
            {
                return BadRequest("Add at leat 1 Address");
            }

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


                //Address
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
                            City = d.City,
                            AppUserId = objectId
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
                //Family
                if (appUserRequest.Families != null)
                {
                    foreach (var f in appUserRequest.Families)
                    {
                        var existFamily = await (await _familyService.GetDbSet())
                                            .FirstOrDefaultAsync(x => x.AppUserId == objectId && x.Id == f.Id);

                        Guid? AddressId = null;
                        //Adding new Member
                        if (existFamily == null)
                        {
                            var newFamAddress = new OLA.Data.Models.User.Address
                            {
                                Barangay = f.Address.Barangay,
                                City = f.Address.City,
                                Purok = f.Address.Purok,
                                AppUserId = objectId

                            };
                            await _addressService.Add(newFamAddress);
                            await _addressService.SaveChangesAsync(Guid.Parse(objectId));

                            AddressId = newFamAddress.Id;

                            var newFamily = new OLA.Data.Models.User.Family
                            {
                                Name = f.Name,
                                ContactNumber = f.ContactNumber,
                                RelationType = (RelationshipType)(int)f.RelationType,
                                DateOfBrith = f.DateOfBrith,
                                IsEmergencyContact = f.IsEmergencyContact,
                                AppUserId = objectId,
                                AddressId = AddressId
                            };

                            await _familyService.Add(newFamily);
                            await _familyService.SaveChangesAsync(Guid.Parse(objectId));
                        }
                        else
                        {
                            var existingAddress = await (await _addressService.GetDbSet())
                                    .FirstOrDefaultAsync(a => a.Id == f.Address.Id && a.AppUserId == objectId);

                            if(existingAddress == null)
                            {
                                var newFamAddress = new OLA.Data.Models.User.Address
                                {
                                    Barangay = f.Address.Barangay,
                                    City = f.Address.City,
                                    Purok = f.Address.Purok
                                };
                                await _addressService.Add(newFamAddress);
                                await _addressService.SaveChangesAsync(Guid.Parse(objectId));
                                AddressId = newFamAddress.Id;
                            }
                            else
                            {
                                // update existing address
                                existingAddress.Barangay = f.Address.Barangay;
                                existingAddress.City = f.Address.City;
                                existingAddress.Purok = f.Address.Purok;
                                await _addressService.Update(existingAddress);
                                await _addressService.SaveChangesAsync(Guid.Parse(objectId));
                                AddressId = existingAddress.Id;
                            }

                            existFamily.Name = f.Name;
                            existFamily.ContactNumber = f.ContactNumber;
                            existFamily.RelationType = (RelationshipType)(int)f.RelationType;
                            existFamily.DateOfBrith = f.DateOfBrith;
                            existFamily.IsEmergencyContact = f.IsEmergencyContact;
                            existFamily.AddressId = AddressId;

                            await _familyService.Update(existFamily);
                            await _familyService.SaveChangesAsync(Guid.Parse(objectId));
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            return Ok(appUserRequest);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var objectId = await _authService.GetUserIdByEmail(email);

            var user = await _authService.GetDbSet()
                        .Include(a => a.Addresses)
                        .Include(f => f.Families)
                            .ThenInclude(fa => fa.Address)
                        .FirstOrDefaultAsync(e => e.Id == objectId);

                            if (user == null)
                                return NotFound("User not found.");

                            var response = user.ToAccountResponse();

                            return Ok(response);
        }
    }
}
