using Application.AppServices.LookupService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupService _lookupService;

        public LookupsController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }
        [AllowAnonymous]  // Allow anonymous access to this endpoint for getting service categories
        [HttpGet("GetCategories")]  //api/Lookups/service-categories
        public async Task<IActionResult> GetAllServiceCategories()
        {
            var categories = await _lookupService.GetAllServiceCategoriesAsync();  //Call the method from the service to get all service categories
            return Ok(categories);  //Return the list of service categories in the response
        }
    }
}
