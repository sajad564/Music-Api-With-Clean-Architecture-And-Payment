using System.Threading.Tasks;
using music.Domain.Common;
using music.Domain.Entities;

namespace music.Domain.Repositories
{
    public interface IGradeRepository : IBaseRepository<Grade>
    {
        Task<Grade> GetByAlbumIdAsync(string albumId) ; 
        Task<Grade> GetByMusicIdAsync(string musicId) ;  
        Task<Grade> GetBySignerIdAsync(string signerId) ; 
    }
}