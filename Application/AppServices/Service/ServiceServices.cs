using Application.AppServices.AuthService.CurrentUserService;
using Application.AppServices.Service.DTOs;
using Application.Repositories;
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

        public async Task CreateService(CreateServiceRequest request)
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
    }
}
