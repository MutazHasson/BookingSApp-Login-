using Application.AppServices.ClientUserService.DTOs;
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
    }
}
