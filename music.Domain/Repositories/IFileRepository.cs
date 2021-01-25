using System.Threading.Tasks;
using music.Domain.Common;
using music.Domain.Entities;

namespace music.Domain.Repositories
{
    public interface IFileRepository : IBaseRepository<File>
    {
        Task<File> GetFileByMusicId(string Id , QualityEnum quality) ; 
        Task<File> GetFileByAlbumId(string Id , QualityEnum quality) ; 
    }
}