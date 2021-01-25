using System.Threading.Tasks;
using music.Domain.Common;
using music.Domain.Entities;

namespace music.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByUsernameAsync(string Username) ;
        Task<User> GetUserByEmailAsync(string Email) ; 
        Task<User> GetUserByIdAsync(string Id) ;
    }
}