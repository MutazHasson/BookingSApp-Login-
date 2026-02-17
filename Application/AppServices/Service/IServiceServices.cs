using Application.AppServices.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.Service
{
    public interface IServiceServices
    {
        Task CreateService(CreateServiceRequest request);
    }
}
