using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.AuthService.CurrentUserService
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        int? ServiceProviderId { get; }
        string? Name { get; }
        string? Email { get; }
        string? MobilePhone { get; }
        string? Role {  get; }
    }
}


//Implementation of IcurrentUserService is the only service that will be implemented in infrastrucure