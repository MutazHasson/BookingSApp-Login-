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
        [HttpPost("Register")]  //api/ServiceProvider/register
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

        [Authorize(Roles = "ServiceProvider")]  // Require authorization to access this endpoint
        [HttpGet("GetAccount")]  //api/ServiceProvider/GetAccount or MyAccount
        public async Task<IActionResult> GetServiceProviderAccount()
        {
            try
            {
                var response = await _serviceProviderService.GetServiceProviderAccount();  //Call the method to get the service provider account details
                return Ok(response);  //Return the response object with the service provider account details
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  //Return bad request with the error message if there is an exception
            }
        }

        // Update service provider account is not implemented yet but it will be similar to the registration method with some differences in the validation and the update process
        [Authorize(Roles = "ServiceProvider")]  // Require authorization to access this endpoint]
        [HttpPut("UpdateAccount")]  //api/ServiceProvider/UpdateMyAccount
        public async Task<IActionResult> UpdateServiceProviderAccount([FromBody] ServiceProviderRegisterationRequest request)
        {
            try
            {
                await _serviceProviderService.UpdateServiceProviderAccount(request);  //Call the method to update the service provider account details
                return Ok("Service provider account updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  //Return bad request with the error message if there is an exception
            }


        }
    }
}
