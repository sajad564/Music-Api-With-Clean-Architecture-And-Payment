using System.Collections.Generic;
using System.Threading.Tasks;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.PaginationParams ; 
namespace music.Domain.Repositories
{
    public interface IMusicRepository : IBaseRepository<Music>
    {
        Task<Music> GetMusicById(string Id) ; 
        PagedList<Music> GetMusicPerPage(MusicPaginationParams paginationParams)  ; 
        
    }
}