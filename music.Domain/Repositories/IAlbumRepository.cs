using System.Threading.Tasks;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.PaginationParams;

namespace music.Domain.Repositories
{
    public interface IAlbumRepository : IBaseRepository<Album>
    {
        Task<Album> GetAlbumById(string Id) ; 
        PagedList<Album> GetAlbumPerPage(AlbumPaginationParams paginationParams) ; 
    }
}