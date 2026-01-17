using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ClientUser
    {
        public int Id { get; set; }

        public DateTime BirthDate { get; set; }//ServiceProvider does not have birthDate, WHY? Cause it could be the provider is 
        // A company Not User
        public int UsertId { get; set; }  // To create foreign key
        public User User { get; set; }  // To create foreign key
    }
}
