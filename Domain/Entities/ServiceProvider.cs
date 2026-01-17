using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ServiceProvider
    {
        public int Id { get; set; }
        public int ServiceCategoryId { get; set; }
        public ServiceCategory ServiceCategory { get; set; }
        public string UserId { get; set; }  // To create Foreign key

        public User User { get; set; }    // To create Foreign key

        public DateTime? AvailableFrom { get; set; }  // Availablity 
        public DateTime? AvailableTo { get; set; } //Let it nullable, so user can do later on./ No manditory
    }
}


// This means UserId will be Primary Key and ServiceCategoryId is Foreign Key. 
// In another word, User is a Parent and ServiceCategory is a one of its children 