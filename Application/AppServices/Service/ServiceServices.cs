using Application.AppServices.AuthService.CurrentUserService;
using Application.AppServices.Service.DTOs;
using Application.Generic_DTOs;
using Application.Repositories;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.AppServices.Service
{
    public class ServiceServices : IServiceServices
    {
        private readonly IGenericRepository<Domain.Entities.Service> _serviceRepo;
        private readonly ICurrentUserService _currentUserService;
        
        public ServiceServices(IGenericRepository<Domain.Entities.Service> serviceRepo, ICurrentUserService currentUserService) 
        { 
            _serviceRepo = serviceRepo;
            _currentUserService = currentUserService;
        }

        public async Task CreateService(SaveServiceRequest request)
        {
            var service = new Domain.Entities.Service()
            {
                Name = request.Name,
                ServiceProviderId = _currentUserService.ServiceProviderId.Value,
                Duration = request.Duration,
                Price = request.Price,
                Description = request.Description,
            };
            await _serviceRepo.InsertAsync(service);
            await _serviceRepo.SaveChangesAsync();


        }

        public async Task DeleteService(int id)
        {
            var service = await _serviceRepo.GetByIdAsync(id);
            if (service != null)
            {
                throw new Exception("Service is not Exist");
            };

            _serviceRepo.Remove(service);
            await _serviceRepo.SaveChangesAsync();

            
        }

        public async Task<PaginationResponse<GetServiceResponse>> GetMyServices(PaginationRequest request)
        {
            var serviceProviderId = _currentUserService.ServiceProviderId.Value;

            var query = _serviceRepo.GetAll().OrderByDescending(x => x.Id)
                 .Where(x => x.ServiceProviderId == serviceProviderId)
                .Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize);

            var count = await query.CountAsync();
                
            var result = await query.Select(x => new GetServiceResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Duration = x.Duration,
                Price = x.Price,

            }).ToListAsync();

            return new PaginationResponse<GetServiceResponse>
            {
                Items = result,
                Count = count,

            };
            
        }

        public async Task UpdateService(int id, SaveServiceRequest request)
        {
            // Call the objext 
            var service = await _serviceRepo.GetByIdAsync(id);

            service.Name = request.Name;
            service.Price = request.Price;
            service.Duration = request.Duration;
            service.Description = request.Description;

            _serviceRepo.Update(service);
            await _serviceRepo.SaveChangesAsync(); 

        }
    }
}
