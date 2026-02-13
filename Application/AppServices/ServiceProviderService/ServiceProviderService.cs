using Application.AppServices.AuthService.CurrentUserService;
using Application.AppServices.ServiceProviderService.DTOs;
using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.ServiceProviderService
{
    public class ServiceProviderService : IServiceProviderService    //Register the service in program.cs
    {
        private readonly IGenericRepository<ServiceProvider> _serviceProviderRepo;
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<Role> _roleRepo;
        private readonly ICurrentUserService _currentUserService; // Assuming you have a service to get the current user
        public ServiceProviderService(IGenericRepository<ServiceProvider> serviceProviderRepo, IGenericRepository<User> userRepo, IGenericRepository<Role> roleRepo, ICurrentUserService currentUserService)
        {
            _serviceProviderRepo = serviceProviderRepo;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _currentUserService = currentUserService;
        }

        public async Task<GetServiceProviderAccountResponse> GetServiceProviderAccount()
        {
            // Get the current user id from the current user service
            var userId = _currentUserService.UserId;
            // Get the service provider account information based on the user id
            var serviceProvider = await _serviceProviderRepo.GetAll()
                .Include(sp => sp.User)
                .FirstOrDefaultAsync(sp => sp.UserId == userId);
            if (serviceProvider == null)
            {
                throw new Exception("Service provider account not found");
            }
            // Map the service provider account information to the response DTO
            // We can use AutoMapper here if we have it set up, but for simplicity, we'll do it manually
            var response = new GetServiceProviderAccountResponse
            {
                Id = serviceProvider.Id,
                Name = serviceProvider.User.Name,
                Email = serviceProvider.User.Email,
                PhoneNumber = serviceProvider.User.PhoneNumber,
                //Password = serviceProvider.User.Password, // Note: In a real application, you should not return the password hash
                serviceCategoryId = serviceProvider.ServiceCategoryId
            };
            return response;
        }



        public async Task serviceProviderRegistration(ServiceProviderRegisterationRequest request)
        {  await RegistrationValidation(request);  //Call the validation method to check if the email or phone number already exist
            var serviceProviderRole = await _roleRepo.GetAll().FirstOrDefaultAsync(x => x.Code == SystemRole.ServiceProvider);  //Get the role of service provider from the database
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                RoleId = serviceProviderRole.Id  // Assuming RoleId 2 is for service providers


            }; 
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, request.Password);  //Hash the password before saving to the database
            await _userRepo.InsertAsync(user);  //Insert the user to the database
            await _userRepo.SaveChangesAsync();  //Save the changes to the database
                var serviceProvider = new ServiceProvider
                {
                    UserId = user.Id,  // Set the UserId to the Id of the newly created user
                    ServiceCategoryId = request.serviceCategoryId  // Set the ServiceCategoryId to the value from the request
                };
                await _serviceProviderRepo.InsertAsync(serviceProvider);  //Insert the service provider to the database
                await _serviceProviderRepo.SaveChangesAsync();  //Save the changes to the database

        }

        // We can implement the update method later, for now we will just throw a not implemented exception
        public async Task UpdateServiceProviderAccount(ServiceProviderRegisterationRequest request)
        {
            var userId = _currentUserService.UserId;  // Get the current user id from the current user service

            await RegistrationValidation(request, userId);  // Call the validation method to check if the email or phone number already exist for other users excluding the current user

            var user = await _userRepo.GetByIdAsync(userId.Value);  // Get the user from the database by id
            var serviceProvider = await _serviceProviderRepo.GetAll().FirstOrDefaultAsync(x => x.UserId == userId);  // Get the client user from the database by user id
            if (serviceProvider == null)
            {
                throw new Exception("Client user not found");
            }
            if (user == null)  // If the user is not found do the following
            {
                throw new Exception("User not found");
            }
            // Update the user properties with the new values from the request
            user.Name = request.Name;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            

            _userRepo.Update(user);  // Update the user in the database
            await _userRepo.SaveChangesAsync();  // Save the changes to the database

            serviceProvider.ServiceCategoryId = request.serviceCategoryId;  // Update
            _serviceProviderRepo.Update(serviceProvider);  // Update the client user in the database
            await _serviceProviderRepo.SaveChangesAsync();  // Save the changes to the database
        }




        private async Task RegistrationValidation(ServiceProviderRegisterationRequest request, int? id = null)
        {
            if (id == null)
            {
                // First: 8 validation) Check user IsExist or New
                var isEmailExist = await _userRepo.GetAll().AnyAsync(x => x.Email == request.Email);
                if (isEmailExist)  //If Email exist do the following
                {
                    throw new Exception("Email already exists");
                }
                var isPhoneNumbrtExist = await _userRepo.GetAll().AnyAsync(x => x.PhoneNumber == request.PhoneNumber);
                if (isPhoneNumbrtExist)  //If PhoneNumber exist do the following
                {
                    throw new Exception("Phone number already exists");
                }
            }
            else
            {
                // Update validation
                var isEmailExist = await _userRepo.GetAll().AnyAsync(x => x.Email == request.Email && x.Id != id.Value);
                if (isEmailExist)  //If Email exist do the following
                {
                    throw new Exception("Email already exists");
                }
                var isPhoneNumbrtExist = await _userRepo.GetAll().AnyAsync(x => x.PhoneNumber == request.PhoneNumber && x.Id != id.Value);
                if (isPhoneNumbrtExist)  //If PhoneNumber exist do the following
                {
                    throw new Exception("Phone number already exists");
                }


            }

        }
    }
}

//AnyAsync or any returns bool (this means if you finds any email same as this email return true

//We always have to register our services in program.cs