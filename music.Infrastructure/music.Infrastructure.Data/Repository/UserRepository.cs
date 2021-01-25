using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using music.Domain.Entities;
using music.Domain.Repositories;
using music.Infrastructure.Data.Data;

namespace music.Infrastructure.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public UserRepository(MusicDbContext _context) : base(_context)
        {
        }
        

        public async Task<User> GetUserByEmailAsync(string Email)
        {
            return await FindByExpression(u => u.Email == Email).Include(u => u.Photo).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByIdAsync(string Id)
        {
            return await FindByExpression(u => u.Id == Id).Include(u => u.Photo).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsernameAsync(string Username)
        {
            return await FindByExpression(u => u.UserName == Username).FirstOrDefaultAsync();
        }
    }
}