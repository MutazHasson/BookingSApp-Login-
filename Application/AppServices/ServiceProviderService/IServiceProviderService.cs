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
        Task<GetServiceProviderAccountResponse> GetServiceProviderAccount(); // To get the account details of the service provider, WE will use current user id to get the details of the service provider
        // for security reason, we will not ask for the service provider id in the request, we will get it from the current user service, becaue we want to make sure that the service provider can only access
        // his own account details, and not the details of other service providers.
    }
}


// Important Notes:
// 1- We create an interface for the service provider service, this interface will be implemented in the service provider service class. This is a good practice to follow, because it allows us to define the
// contract for the service provider service, and it also allows us to use dependency injection to inject the service provider service into the presentation layer.