using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using music.Domain.Entities;
using music.Domain.Repositories;
using music.Infrastructure.Data.Data;

namespace music.Infrastructure.Data.Repository
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {
        public FileRepository(MusicDbContext db) : base(db)
        {
            
        }
        public async Task<File> GetFileByAlbumId(string Id , QualityEnum quality)
        {
            return await FindByExpression(file => file.MusicId==Id && file.Quality==quality).FirstOrDefaultAsync() ;
        }

        public async Task<File> GetFileByMusicId(string Id , QualityEnum quality)
        {
            return await FindByExpression(file => file.Quality==quality&& file.Id==Id).FirstOrDefaultAsync() ;
        }
    }
}