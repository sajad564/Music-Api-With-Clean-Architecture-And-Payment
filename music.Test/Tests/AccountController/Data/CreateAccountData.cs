using System;
using System.Collections;
using System.Collections.Generic;
using music.Api.Dtos;
using Xunit;
using System.Linq ; 
namespace music.Test.Tests.AccountController.Data
{
    public class CreateAccountData : TheoryData<RegisterUserDto>
    {
        public CreateAccountData()
        {
                for(int i=0 ; i<5;i++)
                {
                    Add(RegisterUserData()) ;
                }
        }
        private RegisterUserDto RegisterUserData()
        {
                var uniqueValue = Guid.NewGuid() ;
                var newUser = new RegisterUserDto
                {
                    Username = $"sajad{uniqueValue}" , 
                    Firstname = "sajad" , 
                    Lastname = "amiri" , 
                    Email = $"sajadamiri564{uniqueValue}@gmail.com" ,  // my real gmail : sajadamiri564@gmail.com
                    Password = uniqueValue.ToString() 
                } ; 
                return newUser ;   
        }

    }
}