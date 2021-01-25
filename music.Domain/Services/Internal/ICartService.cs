using System.Threading.Tasks;
using music.Domain.Common;
using music.Domain.Entities;

namespace music.Domain.Services.Internal
{
    public interface ICartService : IBaseService
    {
        Task<Error<Cart>> GetCartById(string cartId) ; 
        Task<Error> AddMusicToCart(string musicId , string CartId) ;
         
        Task<Error> AddAlbumToCart(string albumId, string CartId ) ; 
        Task<Error> RemoveCartItem(string itemId) ; 
        Task<Cart> GetCurrenUserCart() ; 
    }
}