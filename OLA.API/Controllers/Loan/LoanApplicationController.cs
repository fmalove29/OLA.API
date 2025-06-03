using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using OLA.API.Models.request.Loan;
using OLA.Business.Service.Loan;
using OLA.Business.Service.Auth;

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


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllApplication()
        {
            var appList = await _loanApplicationService.GetAllAsync();
            return Ok(appList);
        }

        [Authorize]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(Guid Id)
        {
            var application = await _loanApplicationService.GetByIdAsync(Id);
            return Ok(application);
        }

        //[Authorize]
        //[HttpGet("userLoans")]
        //public async Task<IActionResult> GetByCondition([FromQuery] string objectId)
        //=> Ok(await _authService.GetDbSet())
        //    .Include(e => e.)

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Apply([FromBody] LoanApplicationRequest loanApplicationRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var objectId = await _authService.GetUserIdByEmail(email);
            try
            {
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
