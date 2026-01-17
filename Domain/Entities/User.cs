using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]  // Used the keyword requierd to make field manditory to enter data
        [StringLength(500)] //StringLength is very important to avoid using or consumption of size in Database with no reason.  
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(20)]

        public string PhoneNumber { get; set; }
        [Required]

        public string Password { get; set; }
        //We dont use StringLength with password, cause password has a different case. It will be encrepted

        public int RoleId { get; set; } //Connecting user with Role 

        public Role Role { get; set; }
    }
}


//Now we creating a relation between User and role entity