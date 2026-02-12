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

namespace Application.AppServices.ClientUserService.DTOs
{
    public class ClientUserService : IClientUserService
    {

        private readonly IGenericRepository<ClientUser> _clientUserRepo;
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<Role> _roleRepo;
        public ClientUserService(IGenericRepository<ClientUser> clientUserRepo, IGenericRepository<User> userRepo, IGenericRepository<Role> roleRepo)
        {
            _clientUserRepo = clientUserRepo;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }

        public async Task ClientUserRegisterationAsync(ClientUserRegisterationRequest request)
        {
            await RegistrationValidation(request);  //Call the validation method to check if the email or phone number already exist
            var clientUserRole = await _roleRepo.GetAll().FirstOrDefaultAsync(x => x.Code == SystemRole.User);  //Get the role of service provider from the database
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                RoleId = clientUserRole.Id  // Assuming RoleId 2 is for service providers


            };
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, request.Password);  //Hash the password before saving to the database
            await _userRepo.InsertAsync(user);  //Insert the user to the database
            await _userRepo.SaveChangesAsync();  //Save the changes to the database
            var clientUser = new ClientUser
            {
                UserId = user.Id,  // Set the UserId to the Id of the newly created user
                BirthDate = request.BirthDate
            };
            await _clientUserRepo.InsertAsync(clientUser);  //Insert the service provider to the database
            await _clientUserRepo.SaveChangesAsync();  //Save the changes to the database
        }


        private async Task RegistrationValidation(ClientUserRegisterationRequest request)
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
    }
}