using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.ServiceProviderService.DTOs
{
    public class ServiceProviderRegisterationRequest    //DTO Data trasfer object
    {  //This dto will be a parameter in IserviceProvider
        //Here we add just the basic data
        public string Name { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public int serviceCategoryId { get; set; }

    }
}


//We use DTO to transfer data between the layers
// We send only data that we want send or recieve 
//Lets go the User Entity, and select the properities or data that, we can use in ServiceProviderRegisterationDTO
//In Registeration we use the basic data 