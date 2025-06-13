using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OLA.Business.Service.Administrator;
using Microsoft.AspNetCore.Authorization;
using OLA.API.Models.Filter;
using OLA.API.Extension.Administrator;


namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IAccessService _accessService;

        public AccessController(IAccessService accessService)
        {
            _accessService = accessService;
        }


        //[Authorize]
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




    }
}
