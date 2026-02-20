using Application.AppServices.Service.DTOs;
using Application.Generic_DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.Service
{
    public interface IServiceServices
    {
        Task <PaginationResponse<GetServiceResponse>> GetMyServices(PaginationRequest request);
        Task CreateService(SaveServiceRequest request);
        Task UpdateService(int id, SaveServiceRequest request);
        Task DeleteService(int id);
    }
}
