using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppServices.AuthService.DTOs
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
     

        public string PhoneNumber { get; set; } 

        public SystemRole RoleCode { get; set; }
        //It takes accessToken and refreshToken
        public string AccessToken { get; set; }  //Related to Jwt 
        public string RefreshToken { get; set; }

        //public static implicit operator LoginResponseDto(LoginResponseDto v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

//This is a response that we want 