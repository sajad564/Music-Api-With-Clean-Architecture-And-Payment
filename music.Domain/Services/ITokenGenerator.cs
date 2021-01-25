using System.Threading.Tasks;
using music.Domain.Entities;

namespace music.Domain.Services
{
   public interface ITokenGenerator
    {
         Task<Token> GenerateTokenAsync(User user) ;
          string GetUserIdFromExpiredToken(string token) ;
    }
}