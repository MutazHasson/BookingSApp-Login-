using Application.AppServices.ServiceProviderService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.ServiceProviderService
{
    public interface IServiceProviderService
    {
        //This means, we create a method that sends a request to get data. This method take a dto parameter
        //Implementation will be in serviceProviderService
        Task serviceProviderRegistration(ServiceProviderRegisterationRequest request);
    }
}
