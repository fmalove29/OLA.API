using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using OLA.API.Models.request.Loan;
using OLA.Business.Service.Loan;
using OLA.Business.Service.Auth;
using OLA.API.Models.Filter;
using OLA.API.Models.response.user;
using OLA.API.Models.response.loan;
using OLA.API.Models;
using Microsoft.EntityFrameworkCore;


namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanApplicationController : ControllerBase
    {
        private readonly LoanApplicationService _loanApplicationService;
        private readonly IAuthService _authService;
        public LoanApplicationController(LoanApplicationService loanApplicationService, IAuthService authService)
        {
            _loanApplicationService = loanApplicationService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _loanApplicationService.GetAllAsync());

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> GetLoanApplications([FromQuery] LoanFilter filter)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var objectId = await _authService.GetUserIdByEmail(email);
            var loggedUser = await _authService.GetByIdAsync(objectId);

            var isAdmin = await _authService.IsAdmin(loggedUser);

            var loans = (await _loanApplicationService.GetDbSet())
                .Include(e => e.AppUser)
                .AsEnumerable()
                .Where(e =>
                    // If Admin: no restriction, else: only loans created by this user
                    (isAdmin || e.AppUserId == loggedUser.Id) &&
                    (
                        string.IsNullOrEmpty(filter.UserName) ||
                        (e.AppUser.FirstName?.ToLower().Contains(filter.UserName.ToLower()) ?? false) ||
                        (e.AppUser.LastName?.ToLower().Contains(filter.UserName.ToLower()) ?? false) ||
                        (e.AppUser.MiddleName?.ToLower().Contains(filter.UserName.ToLower()) ?? false)
                    )
                )
                .Select(e => new LoanApplicationResponse
                {
                    AmountRequested = e.AmountRequested,
                    TermsInDays = e.TermsInDays,
                    Purpose = e.Purpose,
                    Notes = e.Notes,
                    AppUser = new AppUserResponse
                    {
                        FirstName = e.AppUser.FirstName,
                        LastName = e.AppUser.LastName,
                        MiddleName = e.AppUser.MiddleName,
                        UserName = e.AppUser.UserName,
                        Email = e.AppUser.Email,
                        PhoneNumber = e.AppUser.PhoneNumber,
                        Addresses = e.AppUser.Addresses?
                            .Select(a => new AddressResponse
                            {
                                Barangay = a.Barangay,
                                City = a.City,
                                Purok = a.Purok
                            }).ToList() ?? new List<AddressResponse>(),
                        Families = e.AppUser.Families?
                            .Select(f => new FamilyResponse
                            {
                                Name = f.Name,
                                ContactNumber = f.ContactNumber,
                                RelationshipType = (OLA.API.Models.RelationshipType)f.RelationType,
                                RelationshipTypeName = f.RelationshipTypeName,
                                DateOfBrith = f.DateOfBrith,
                                IsEmergencyContact = f.IsEmergencyContact,
                                Address = new AddressResponse
                                {
                                    Barangay = f.Address?.Barangay,
                                    Purok = f.Address?.Purok,
                                    City = f.Address?.City
                                }
                            }).ToList() ?? new List<FamilyResponse>()
                    }
                })
                .ToList();

            return Ok(loans);
        }


        [Authorize]
        [HttpGet("getbyuserlogged")]
        public async Task<IActionResult> GetLoanByUserLogged()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var objectId = await _authService.GetUserIdByEmail(email);
            var loans = (await _loanApplicationService.GetDbSet())
                        .AsEnumerable()
                        .Where(e => e.AppUserId == objectId)
                        .Select(l  => new LoanApplicationResponse
                        {
                            TermsInDays = l.TermsInDays,
                            Notes = l.Notes,
                            AmountRequested = l.AmountRequested,
                            Purpose = l.Purpose
                        })
                        .ToList();

            return Ok(loans);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Apply([FromBody] LoanApplicationRequest loanApplicationRequest)
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
                {
                    return NotFound(new { message = "User not found." });
                }

                if (findUser.Addresses == null || !findUser.Addresses.Any() ||
                    findUser.Families == null || !findUser.Families.Any())
                {
                    return BadRequest(new { message = "Please update Address Details and Family Details." });
                }



                //if (findUser == null)
                //{
                //    return NotFound(new { message = "User not found." });
                //}

                //if (!findUser.Addresses.Any() || !findUser.Families.Any())
                //{
                //    return BadRequest(new { message = "Please update Address Details and Family Details." });
                //}

                //return Ok(findUser);

                var app = new OLA.Data.Models.Loan.LoanApplication
                {
                    AppUserId = objectId,
                    AmountRequested = loanApplicationRequest.AmountRequested,
                    TermsInDays = loanApplicationRequest.TermsInDays,
                    Purpose = loanApplicationRequest.Purpose,
                    Notes = loanApplicationRequest.Notes
                };

                
                await _loanApplicationService.AddAsync(app);
                await _loanApplicationService.SaveChangesAsync(Guid.Parse(objectId));

                return Ok(new { message = "Application submitted successfully " });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        
    }
}
