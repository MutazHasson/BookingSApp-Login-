using Application.AppServices.ServiceProviderService;
using Application.AppServices.ServiceProviderService.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProviderController : ControllerBase
    {
        // Dependency Injection for ServiceProviderService to use its methods in the controller
        // We inject the service in the constructor to use it in the controller
        private readonly IServiceProviderService _serviceProviderService;
        // Constructor
        public ServiceProviderController(IServiceProviderService serviceProviderService)
        {
            _serviceProviderService = serviceProviderService;
        }

        [AllowAnonymous]  // Allow anonymous access to this endpoint for registration
        [HttpPost("register")]  //api/ServiceProvider/register
        public async Task<IActionResult> RegisterServiceProvider([FromBody] ServiceProviderRegisterationRequest request)
        {
            try
            {
                await _serviceProviderService.serviceProviderRegistration(request);  //Call the registration method from the service
                return Ok("Service provider registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  //Return bad request with the error message if there is an exception
            }
        }
    }
}
