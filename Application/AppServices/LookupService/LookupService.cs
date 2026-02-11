using Application.AppServices.LookupService.DTOs;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.LookupService
{
    public class LookupService : ILookupService
    {
        private readonly IGenericRepository<ServiceCategory> _serviceCategoryRepo;

        public LookupService(IGenericRepository<ServiceCategory> serviceCategoryRepo)
        {
            _serviceCategoryRepo = serviceCategoryRepo;
        }

        public async Task<List<GetLookupResponse>> GetAllServiceCategoriesAsync()
        {
            var categories = await _serviceCategoryRepo.GetAll().Select(sc => new
            GetLookupResponse  // Map the ServiceCategory entity to the GetLookupResponse DTO
            {
                Id = sc.Id,
                Name = sc.Name,
                Code = sc.Code
            }).ToListAsync();
            return categories;
        }
               // Fetch all service categories from the repository (DB) and convert to a list asynchronously
    }
    
}

//Map the ServiceCategory entity to the GetLookupResponse DTO using LINQ's Select method and return the list of GetLookupResponse objects.
// We use select to project each ServiceCategory entity into a new GetLookupResponse object, which contains only the properties we want to expose in the response.