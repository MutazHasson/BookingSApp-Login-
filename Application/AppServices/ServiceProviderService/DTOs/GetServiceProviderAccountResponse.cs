using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.ServiceProviderService.DTOs
{
    public class GetServiceProviderAccountResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public string Password { get; set; }    // Note: In a real application, you should not return the password hash Should be removed for security reasons, but it is included here for demonstration purposes only.
        // we can write address , description, rating, etc... if we want to display more information about the service provider in the presentation layer.

        public int serviceCategoryId { get; set; }
    }
}
