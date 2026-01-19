using Domain.Entities;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class UserSeedData  //Inside static class everything should be static
    {
        private readonly static string adminPassword = "Admin@123";  //Default admin password
        public static void UserSeed(IServiceProvider serviceProvider)  //It will take a parameter
        { 
            using var context = serviceProvider.GetRequiredService<BookingSAppContext>();
            //We use this code in places that, we cant make independancy injection without constructor 
            //Via above code, we cant add user
            //We have to add role first
            if (!context.Roles.Any())  //To check if there is a Role inside the database//Roles brought from BSAContext
            {
                var roles = new List<Role>
                {
                   new Role {Name = "Admin", Code = SystemRole.Admin},
                   new Role { Name = "ServiceProvider", Code = SystemRole.ServiceProvider },
                   new Role { Name = "User", Code = SystemRole.User }

                };
                
                context.Roles.AddRange(roles);  //Using addRange to add list of object
                context.SaveChanges();
            }
            if (!context.Users.Any())  //To check if there is a user inside the database//Users brought from BSAContext
            {
                var adminRoleId = context.Roles.FirstOrDefault(r => r.Code == SystemRole.Admin).Id;
                var user = new User
                {
                    Name = "Admin User",
                    Email = "admin@bsa.com",
                    PhoneNumber = "1234567890",
                    RoleId = adminRoleId  // Get adminRole by Id

                };
                var passwordHasher = new PasswordHasher<User>(); // To make password hashing we need to install// NuGet Package microsoft.extentsions identity 
                user.Password = passwordHasher.HashPassword(user, adminPassword);
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}


// We need to create this class static: Why??
// Then we create function inside the class
//To create data in database, we need DbContext
//Using Scope Service. It allows us to make indepnedancy injection without using constructor 
// What is using, why and how

//Using if Condiction to check if there is No user, We create a user
// Finally, we must call it in programm.cs// Take seedData 

// Next, Make encreption to the password
//We install a package that make encreption hashingPassword