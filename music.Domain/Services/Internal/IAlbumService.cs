using System.Threading.Tasks;
using music.Domain.Common;
using music.Domain.Entities;

namespace music.Domain.Services.Internal
{
    public interface IAlbumService : IBaseService
    {
        Task<Error> AddAlbumAsync(Album newAlbum) ; 
        Task<Error> RemoveAlbumAsync(string albumId)  ; 
        Task<Error<Album>> GetAlbumByIdAsync(string AlbumId) ; 
        
    }
}