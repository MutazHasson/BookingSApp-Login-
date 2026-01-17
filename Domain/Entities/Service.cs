using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Service
    {
        public int Id { get; set; }
        [StringLength(300)]
        public string Name { get; set; }
        public int ServiceProviderId { get; set; }

        public ServiceProvider ServiceProvider { get; set; }

        //public int ServiceCategoryId { get; set; } // later. in case we want add subCategories to the provider
        //public ServiceCategory ServiceCategory { get; set; }// For example. IT-  subCateg is web-Developer,...ect.

        public decimal Price { get; set; }
        public int Duration { get; set; }

        public string Description { get; set; }

    }
}
