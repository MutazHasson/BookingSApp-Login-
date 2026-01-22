using Application.AppServices.AuthService;
using Application.AppServices.AuthService.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;


namespace BookingSApp.Controllers
{
    [Authorize]  //We can put here, this means all methods are authorized
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //Implement authentication methods here
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //Post login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.Login(request);
            if (result == null)
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }
            return Ok(result);
        }
        //RefreshToken
        //[Authorize] // We can give access to admin or any one from seeddata (Authorize means user has loged in
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var newAccessToken = await _authService.GenerateNewAccessToken(refreshToken);
            if (newAccessToken == null)
            {
                return Unauthorized(new { Message = "Invalid refresh token." });
            }
            return Ok(newAccessToken);
        }

        [HttpPost("ChangeMyPassword")]
        public async Task<IActionResult> ChangeMyPassword([FromBody] ChangeMyPasswordRequest request)
        {
            await _authService.ChangeMyPassword(request);
            return Ok();
        }
    }
}
