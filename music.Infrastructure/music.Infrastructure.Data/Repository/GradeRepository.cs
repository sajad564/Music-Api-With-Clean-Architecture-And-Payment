using music.Domain.Entities;
using music.Infrastructure.Data.Data;
using music.Domain.Repositories ;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace music.Infrastructure.Data.Repository
{
    public class GradeRepository : BaseRepository<Grade> , IGradeRepository
    {
        public GradeRepository(MusicDbContext context) : base(context){}

        public async Task<Grade> GetByAlbumIdAsync(string albumId)
        {
            return await db.Grades.Where(g => g.AlbumId==albumId).FirstOrDefaultAsync() ;
        }

        public async Task<Grade> GetByMusicIdAsync(string musicId)
        {
            return await db.Grades.Where(g => g.MusicId==musicId).FirstOrDefaultAsync() ; 
        }

        public async Task<Grade> GetBySignerIdAsync(string signerId)
        {
            return await db.Grades.Where(g => g.SignerId==signerId).FirstOrDefaultAsync() ; 
        }
    }
}