using System.Collections.Generic;
using System.Threading.Tasks;
using music.Domain.Common;
using music.Domain.Entities;

namespace music.Domain.Services.Internal
{
    public interface IUserService 
    {
        Task<Error> AddUser(User newUser , string Password) ; 
        Task<Error<Token>> LoginUser(string Username , string password) ;
        Task<Error<Token>> Refresh(Token token);
        Task<Error> EditUser(User user) ;  
    }
}