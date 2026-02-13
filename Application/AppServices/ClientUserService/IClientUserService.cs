using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.ClientUserService.DTOs
{
    public interface IClientUserService
    {
        Task ClientUserRegisterationAsync(ClientUserRegisterationRequest request);
        Task<GetClientUserResponse> GetClientUserAccountAsync();

    }
}
