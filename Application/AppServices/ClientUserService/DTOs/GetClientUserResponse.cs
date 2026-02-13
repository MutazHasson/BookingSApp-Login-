using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.ClientUserService.DTOs
{
    public class GetClientUserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public DateTime BirthDate { get; set; }
    }
}


// We do this after finishing the ClientUserRegisterationRequest, because we want to use the same properties in the response, but we can also add more properties if we want to display more information about the client user in the presentation layer.
// This class is a Data Transfer Object (DTO) that represents the response for getting a client user. It contains properties such as Id, Name, Email, PhoneNumber, Password, and BirthDate. This DTO is used to transfer data from the service layer to the presentation layer when retrieving client user information.
// We give the client user information in the response, so we can use it in the presentation layer to display the client user details. The properties in this DTO should match the properties of the ClientUser entity in the domain layer, but it can also include additional properties if needed for the presentation layer.
// Note: In a real application, you should not include the Password property in the response DTO for security reasons. It is included here for demonstration purposes only.
// This will be in setting page, when the client user want to see his information, he can see it in this page, and if he want to update it, he can update it in the same page.