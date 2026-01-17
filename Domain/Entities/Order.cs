using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }  //Creating a foreign key 

        public Service Service { get; set; }  //Creating a foreign key 
        public int ServiceProviderId { get; set; } // Creating a foreign key 
        public ServiceProvider ServiceProvider { get; set; } // Creating a foreign key
        public int ClientUserId { get; set; } // Creating a foreign key 
        public ClientUser ClientUser { get; set; } // Creating a foreign key
        public DateTime FromTime { get; set; }  
        public DateTime ToTime { get; set; }
        public string? Note { get; set; }   //Nullable, cause its optional to write a note. 
        public OrderStatus Status { get; set; } //Traging status of order
    }
}
