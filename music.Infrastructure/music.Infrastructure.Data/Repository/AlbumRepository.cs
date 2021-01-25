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
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(MusicDbContext _db) : base(_db)
        {
        }

        public async Task<Album> GetAlbumById(string Id)
        {
            return await FindByExpression(album => album.Id==Id).Include(a => a.Photos).Include(a => a.Grade).FirstOrDefaultAsync() ; 
        }

        public PagedList<Album> GetAlbumPerPage(AlbumPaginationParams paginationParams)
        {
            var filteredSource = Filter(paginationParams) ; 
            return PagedList<Album>.ToPagedList(filteredSource , paginationParams.PageSize , paginationParams.PageNumber) ; 
        }
        private IQueryable<Album> Filter(AlbumPaginationParams paginationParams)
        {
            return FindByExpression(a =>
            (a.PublishedData>=paginationParams.MinDateTime)&&
            (a.PublishedData<=paginationParams.MaxDateTime)&&
            (string.IsNullOrEmpty(paginationParams.SignerId) ||a.SignerId==paginationParams.SignerId)&&
            (string.IsNullOrEmpty(paginationParams.Name) || a.Name==paginationParams.Name)&&
            a.IsFree==paginationParams.IsFree
                ).AsQueryable() ; 
        }
    }
}