using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.Repositories;
using music.Infrastructure.Data.Data;

namespace music.Infrastructure.Data.Repository
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(MusicDbContext db) : base(db)
        {}

        public async Task AddPhoto(Photo photo)
        {
            await db.Photos.AddAsync(photo) ; 
        }

        public async Task<IEnumerable<Photo>> GetPhotoByAlbumId(string Id)
        {
            return await FindByExpression(f => f.AlbumId==Id).ToListAsync() ; 
        }

        public  async Task<Photo> GetPhotoByUserId(string Id)
        {
            return await FindByExpression(p => p.UserId==Id).FirstOrDefaultAsync() ; 
        }

        public async Task<IEnumerable<Photo>> GetPhotosByMusicId(string Id)
        {
            return await FindByExpression(p => p.MusicId==Id).ToListAsync() ; 
        }
    }
}