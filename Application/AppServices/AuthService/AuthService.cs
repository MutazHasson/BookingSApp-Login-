using Application.AppServices.AuthService.CurrentUserService;
using Application.AppServices.AuthService.DTOs;
using Application.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.AuthService
{
    public class AuthService : IAuthService
    {
        //Creating dependancy injection
        private readonly IConfiguration _config;
        private readonly ICurrentUserService _currentUserService;
        private readonly IGenericRepository<User> _userRepository;  //Repo is crucial By adding <User> we can get User
        private readonly IGenericRepository<ServiceProvider> _serviceProviderRepository;
        private readonly IGenericRepository<RefreshToken> _RefreshTokenRepository; //Using Refresh token Rep
        //Creating A AuthService Constructor
        public AuthService(IGenericRepository<User> userRepository, IGenericRepository<RefreshToken> refreshTokenRepository, IConfiguration config, ICurrentUserService currentUserService, IGenericRepository<ServiceProvider> serviceProviderRepository)
        {
            _userRepository = userRepository;
            _RefreshTokenRepository = refreshTokenRepository;
            _config = config;
            _currentUserService = currentUserService;
            _serviceProviderRepository = serviceProviderRepository;
        }

        //Login Implementation
        public async Task<LoginResponseDto> Login(LoginRequestDto request)
        {
            //Eager loading vs lazy loading)
            var user = await _userRepository.GetAll()
                .Include(u => u.Role) //Use include to get related data Object Role
                .FirstOrDefaultAsync(u => u.Email == request.UserName.Trim().ToLower() || u.PhoneNumber.Trim() == request.UserName);
                // Check User
                if (user == null)
                {
                //throw new Exception("Invalid username or password."); //Dont inform the user what exactly invalid
                return null;
                }
                var passwordHasher = new PasswordHasher<User>(); // To make password hashing we need to install// NuGet Package microsoft.extentsions identity 
                var passwordResult = passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
            if (passwordResult == PasswordVerificationResult.Failed)
            {
                return null; //Means get out of function
            }
            //Save RefreshToken of the user in table
            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = GenerateRefreshToken(),
                ExpiryDate = DateTime.UtcNow.AddDays(7),
            };
            await _RefreshTokenRepository.AddAsync(refreshToken);
            await _RefreshTokenRepository.SaveChangesAsync();

            //LoginResponse
            var result = new LoginResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RoleCode = user.Role.Code,  //Role is related data/ It does not return by default/ we will get an error
                // for this reason we will use keyword Include above Just like in DB
                AccessToken = await GenerateAccessToken(user),
                RefreshToken = refreshToken.Token,

                //RefreshToken = GenerateRefreshToken() // We have to save this in RefreshToken entity that we created previously

            };

            return result;
        }
        // Generate New Access Token
        public async Task<string> GenerateNewAccessToken(string refereToken)
        {
            var token = await _RefreshTokenRepository.GetAll()
                .FirstOrDefaultAsync(t => t.Token == refereToken);
           if (token == null || token.ExpiryDate < DateTime.UtcNow)
            {
                return null;
            }
           var user = await _userRepository.GetAll()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == token.UserId);
            return await GenerateAccessToken(user);
        }
        
        //Update ChangePassword
        public async Task ChangeMyPassword(ChangeMyPasswordRequest request)
        {
            var user = await _userRepository.GetByIdAsync(_currentUserService.UserId.Value); //Error cause Id is? Nullable. To fix it use .Value
            var passwordHasher = new PasswordHasher<User>();
            var passwordResult = passwordHasher.VerifyHashedPassword(user, user.Password, request.CurrentPassword);
            if (passwordResult == PasswordVerificationResult.Failed) 
            {
                throw new Exception("Current password is incorrect");
            }
            if (request.NewPassword != request.ConfirmNewPassword)
            {
                throw new Exception("Confirm password is incorrect");
            }
            user.Password = passwordHasher.HashPassword(user, request.NewPassword);
        }


        //Cope Pase Generate AccessToken
        private async Task<string> GenerateAccessToken(User user)
        {
            var jwtSection = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), //Claim takes always string
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Role, user.Role.Name)
                
            };

            var serviceProviderUser = _serviceProviderRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (serviceProviderUser != null) //It returns null, otherwise we add new claim
            {
                claims.Add(new Claim("ServiceProviderId", serviceProviderUser.Id.ToString()));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = jwtSection["Issuer"],
                Audience = jwtSection["Audience"],
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);

        }
        //GenerateRefreshToken
        private string GenerateRefreshToken()
        {
            var random = new byte[64];
            RandomNumberGenerator.Fill(random);
            return Convert.ToBase64String(random);
        }
    }
}


// Login will be via JSON Web Token (JWT) is considered as token, but what is a token is a group of strings
//With no meaning its encrypted 


//GetAll gets or returns only one element, if we want all list, we will add toList()

//If we want to get any related data in entity framework, we have to use Inclued(u => u. ...), otherwise, we will get
// Null Example in our case ROLE object
//Include represents left join
//We have 3 types of fechting data //Eager loading, lazy loading and explicit loading 
// Search Eager loading && lazy loading 

//JWT => access  should be less 15 minutes
// RefreshToken standard 7 days


// JWT is a container that carries claims 
//Claim is simply a piece of information about a current User. (E.g UserId, Email Role, permissions)
//Why claims?? Cause API needs to critical questions Who is this user? What can this user do? What data belongs to this user? 
//a Claim is a key-value pair

//To Update password, we need the password, we can take the password of current User form the claim. cause claim get 
// The information 