using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using music.Domain.Services.Internal;

namespace music.Services.services
{
    public class BaseService : IBaseService
    {
        private List<string> Roles = new List<string>() ; 
        private HttpContext context {get;set;}
        public BaseService(IHttpContextAccessor accessor)
        {
            context = accessor.HttpContext ;
            Roles =  context.User.FindAll(ClaimTypes.Role).Select(claim => claim.Value).ToList() ;
        }
        

        public string UserId()
        {
            return context.User.FindFirst(ClaimTypes.NameIdentifier).Value ;
        }

        public bool IsInRole(string role)
        {
            return Roles.Where(r => r==role).Any() ; 
        }

        public bool isInOneRole(string role1, string role2)
        {
           return Roles.Where(r => r==role1 || r==role2).Any() ; 
        }

        public (bool role1, bool role2) inWichRole(string role1, string role2)
        {
            bool isInRole1 = Roles.Where(r => r==role1).Any() ; 
            bool isInRole2 = Roles.Where(r =>r==role2).Any() ; 
            return (isInRole1 ,isInRole2) ; 
        }
    }
}