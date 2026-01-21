using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Context
{
    public class BookingSAppContext: DbContext
    {

        // Creating field and consturctor for DbContext
        public BookingSAppContext(DbContextOptions<BookingSAppContext> options) :base(options)
        {
            //Implementation
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //The following code, restrict deleting entities randomly, Know it, dont memorize it. 
            var relationShips = modelBuilder.Model
                .GetEntityTypes().SelectMany(e => e.GetForeignKeys()); // This calls all Data in DB, this

            foreach (var relationship in relationShips) // Looping through all data and restrict them
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict; //Restrict Not cascade
            }
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<ServiceCategory>().HasData(
                     new ServiceCategory{Id = 1, Name = "Pluming", Code = Domain.Enums.ServiceCategoryEnum.pluming},
                     new ServiceCategory { Id = 2, Name = "Electrical", Code = Domain.Enums.ServiceCategoryEnum.Electrical },
                     new ServiceCategory { Id = 3, Name = "Cleaning", Code = Domain.Enums.ServiceCategoryEnum.Cleaning },
                     new ServiceCategory { Id = 4, Name = "Landscaping", Code = Domain.Enums.ServiceCategoryEnum.Landscaping},
                     new ServiceCategory { Id = 5, Name = "Painting", Code = Domain.Enums.ServiceCategoryEnum.Painting },
                     new ServiceCategory { Id = 6, Name = "Carpentry", Code = Domain.Enums.ServiceCategoryEnum.Carpentry},
                     new ServiceCategory { Id = 7, Name = "HVAC", Code = Domain.Enums.ServiceCategoryEnum.HVAC },
                     new ServiceCategory { Id = 8, Name = "Roofing", Code = Domain.Enums.ServiceCategoryEnum.Roofing },
                     new ServiceCategory { Id = 9, Name = "PestControl", Code = Domain.Enums.ServiceCategoryEnum.PestControl },
                     new ServiceCategory { Id = 10, Name = "Moving Services", Code = Domain.Enums.ServiceCategoryEnum.MovingServices }
                ); //If we run service with data, db can not find this seeddata, for this reason we must add migration
        }

        //After getting project reference from Domain tp infrastructure, we have to call the entities objects
        public DbSet<User> Users { get; set; }
        public DbSet<ClientUser> ClientUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<ServiceProvider> ServiceProviders { get; set; }


    }
}


// Important Notes:
// Migration happens always where context is. This means Migration will be inside infrastructure. 
// To make migration we have to install package EntityFrameworkCore Tool via Infrastructure. 
// Before starting with migration, check out if there is an error, to do that select Build in navigation tool above, and then
// Select rebuild solution (Done all good)
// Another important key, We have to choose Infrastructure, where the context exists: 
//Usage: Select Tools, NuGet Package Manager, then  Package manager Console and lastly select Project In our case Infrastructure
// To Forget ConnectionString: Go To Appsettings.json and write connectionString

//Important Notes
// Cascade delete Is FORBIDDEN NOT ALLOWED and dengaras can cost lots. Once can delete whole data: Following an error
//Introducing FOREIGN KEY constraint 'FK_Orders_ServiceProviders_ServiceProviderId' on table 'Orders' may cause
//cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY
//constraints.

//Topics
// Seed Data: are the initial data that come with entity when we create tables in database
//Why:For example in our case, Roles / No one will add the role from outside. So we must create 
//initial data for roles to determine Who and what to do. Same thing with Categories ...(...)
//Two types of seed data: (One runtime, second within migration)

//How to create Seed data:
//We open a new folder in infrastructure called data

// SeedData via Migration (Will be inside OnModelCreaing methode
