using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SystemRole Code { get; set; } //= SystemRole.Admin;
        // Via Conde, we are able to determine and access the userRole (Admin, User, Or Provider)
        // In case we want to get the role later, we can just the code (1, 2 or 3) using numbers safer than string e.g admin

        public ICollection<User> Users { get; set; }  //Create ICollection 
        //Why using User here, cause one user can have many roles


    }
}
