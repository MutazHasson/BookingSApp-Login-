using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        //After getting project reference from Domain in infrastructure, we have to call the entities objects
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