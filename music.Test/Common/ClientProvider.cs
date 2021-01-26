using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using music.Api;
using music.Api.Common;
using music.Api.Dtos;
using music.Domain.Entities;

namespace music.Test.Common
{
    public class ClientProvider 
    {
        private HttpClient _client {get;set;}
        private List<LoginDto> _Users {get;set;}  
        public HttpClient Client {
            get
            {
                if(_client!=null) 
                {
                    return _client ; 
                }
                else
                {
                     _client = new WebApplicationFactory<Startup>().CreateClient() ; 
                    _client.Timeout = TimeSpan.FromSeconds(100) ;
                    return _client ; 
                }
            }
        }
        protected async Task AuthenticateMeBabyAsync()
        {
            InstensiateUsers() ; 
            var token = await GetTokenAsync() ; 
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer" ,token) ; 
        }
        private async Task<string> GetTokenAsync()
        {
            var response = await Client.PostAsJsonAsync("/api/account/login" ,SelectRandomUserForLogin() ) ; 
            var result = await response.Content.ReadAsAsync<Response<Token>>() ;
            return result.Item.AccessToken; 
        }
        private LoginDto SelectRandomUserForLogin()
        {
            var rand = new Random().Next(0,_Users.Count-1) ; 
            return _Users[rand] ; 
        }
        private void InstensiateUsers()
        {
            _Users = new List<LoginDto>()
            { 
              new LoginDto {Username ="ALf@12331" , Password ="123@saJad3"} 
            } ; 
        }
        
    }
}