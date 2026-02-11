using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.LookupService.DTOs
{
    public class GetLookupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ServiceCategoryEnum Code { get; set; }
    }
}
