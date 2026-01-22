using Application.AppServices.AuthService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.AuthService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto request);
        Task<string> GenerateNewAccessToken(string refreshToken);
        Task ChangeMyPassword(ChangeMyPasswordRequest request);
    }
}
