using System.Threading.Tasks;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.PaginationParams;

namespace music.Domain.Services.Internal
{
    public interface IMusicService : IBaseService
    {
        Task<Error<Music>> GetMusicByIdAsync(string musicId) ; 
        PagedList<Music> GetMusicPerPage(MusicPaginationParams paginationParams) ;
        Task<Error> AddMusicAsync(Music music);  
        Task<Error> RemoveMusicAsync(string MusicId) ; 
    }
}