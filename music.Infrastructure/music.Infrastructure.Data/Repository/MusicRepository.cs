using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.PaginationParams;
using music.Domain.Repositories;
using music.Infrastructure.Data.Data;

namespace music.Infrastructure.Data.Repository
{
    public class MusicRepository : BaseRepository<Music>, IMusicRepository
    {
        public MusicRepository(MusicDbContext db) : base(db)
        {
            
        }
        public async Task<Music> GetMusicById(string Id)
        {
            return await FindByExpression(m => m.Id==Id).Include(m => m.Grade).Include(m => m.Signer).Include(m => m.Photos).FirstOrDefaultAsync(); 
        }
        public PagedList<Music> GetMusicPerPage(MusicPaginationParams paginationParams)
        {
            var filteredSource = Filter(paginationParams) ;
            return PagedList<Music>.ToPagedList(filteredSource , paginationParams.PageSize ,paginationParams.PageSize)  ; 
        }
        private IQueryable<Music> Filter(MusicPaginationParams paginationParams)
        {
            return FindByExpression(m => 
                (string.IsNullOrEmpty(paginationParams.Name)|| m.Name==paginationParams.Name) &&
                (string.IsNullOrEmpty(paginationParams.signerId) || m.SignerId==paginationParams.signerId)&&
                (m.IsFree==paginationParams.IsFree) &&
                (paginationParams.minPrice==null || m.Price>=paginationParams.minPrice )&&
                (paginationParams.MaxPrice==null || m.Price<=paginationParams.MaxPrice)&&
                (m.PublishedDate<=paginationParams.MaxDateTime && m.PublishedDate>=paginationParams.MinDateTime )
            ).Include(m => m.Photos).Include(m => m.Grade).AsQueryable() ;
        }
    }
}