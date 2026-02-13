using Application.AppServices.ClientUserService.DTOs;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientUserController : ControllerBase
    {
        // Dependency Injection for ClientUserService to use its methods in the controller
        private readonly IClientUserService _clientUserService;
        // Constructor
        public ClientUserController(IClientUserService clientUserService)
        {
            _clientUserService = clientUserService;
        }

        [AllowAnonymous]
        [HttpPost("register")]  //api/ClientUser/register
        public async Task<IActionResult> RegisterClientUser([FromBody] ClientUserRegisterationRequest request)
        {

            //await _clientUserService.ClientUserRegisterationAsync(request);  //Call the registration method from the service
            // return Ok("Client user registered successfully");
            try
            {
                await _clientUserService.ClientUserRegisterationAsync(request);  //Call the registration method from the service
                return Ok("Client user registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  //Return bad request with the error message if there is an exception
            }
        }
        [Authorize(Roles = "User")]  // Require authorization to access this endpoint
        [HttpGet("GetAccount")]  //api/ClientUser/GetAccount or MyAccount
        public async Task<IActionResult> GetClientUserAccount()
        {
            try
            {
                var response = await _clientUserService.GetClientUserAccountAsync();  //Call the method to get the client user account details
                return Ok(response);  //Return the response object with the client user account details
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  //Return bad request with the error message if there is an exception
            }
        }

        // Update client user account is not implemented yet but it will be similar to the registration method with some differences in the validation and the update process
        [Authorize(Roles = "User")]  // Require authorization to access this endpoint
        [HttpPut("UpdateMyAccount")]  //api/ClientUser/GetAccount or MyAccount
        public async Task<IActionResult> UpdateClientUserAccount([FromBody] ClientUserRegisterationRequest request)
        {
            try
            {
                await _clientUserService.UpdateClientUserAccount(request);  //Call the method to get the client user account details
                return Ok();  //Return the response object with the client user account details
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  //Return bad request with the error message if there is an exception
            }
        }
    }
}
