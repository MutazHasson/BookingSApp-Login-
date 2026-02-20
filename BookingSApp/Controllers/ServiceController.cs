using Application.AppServices.Service;
using Application.AppServices.Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceServices _servicesService;
        public ServiceController(IServiceServices servicesService) { _servicesService = servicesService; }

        [Authorize(Roles = "ServiceProvider")]
    [HttpPost("")]
        public async Task<IActionResult> CreateService([FromBody] SaveServiceRequest request)
        {
            await _servicesService.CreateService(request);
            return Ok();

        }
    }
}
