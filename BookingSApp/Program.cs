using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<BookingSAppContext>(options =>    // Find the reference for Context
options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))); //Install Packages 
//Above code connects dbContext via 

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
