using Application.AppServices.AuthService;
using Application.AppServices.AuthService.CurrentUserService;
using Application.AppServices.ClientUserService.DTOs;
using Application.AppServices.LookupService;
using Application.AppServices.ServiceProviderService;
using Application.Repositories;
using Infrastructure.Context;
using Infrastructure.Data;
using Infrastructure.RepositoriesInf;
using Infrastructure.Services.CurrentUserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 

builder.Services.AddControllers();

builder.Services.AddDbContext<BookingSAppContext>(options =>    // Find the reference for Context
options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))); //Install Packages 
//Above code connects dbContext via 
var jwtSection = builder.Configuration.GetSection("Jwt");  //GetSection should be created in Json
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"])),
        };

    }
    
    );

builder.Services.AddHttpContextAccessor(); //Within THIS we can enter claim or access its members 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BookingSApp API",  //e.g name of the project
        Version = "v1"
    });


    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Put **_ONLY_** your JWT Bearer token hera",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityReq = new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] { } }
    };
    c.AddSecurityRequirement(securityReq);
});
//Services 
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
builder.Services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
builder.Services.AddScoped(typeof(IServiceProviderService), typeof(ServiceProviderService));
builder.Services.AddScoped(typeof(IClientUserService), typeof(ClientUserService));  //Register ClientUserService in program.cs
builder.Services.AddScoped(typeof(ILookupService), typeof(LookupService));  //Register DbContext in program.cs

var app = builder.Build(); 
UserSeedData.UserSeed(app.Services);  //From userSeedData

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//We create seeddata, cause we dont want to use contructor 

//After registering IGenericRepository, we start with LOGIN fuction 
//How things work in clean architecture
//First thing Controller => calls Service => calls Repository 
//Services will be in Application layer (Creating AppServices Folder)
//First service is AuthService with IAuthService
// We have to register services in Programm.cs 


// We have to add JWT configuaration
//To Use swagger with authentication we use The long code CopyPaste

//Before Login Function. we have to create DTOs