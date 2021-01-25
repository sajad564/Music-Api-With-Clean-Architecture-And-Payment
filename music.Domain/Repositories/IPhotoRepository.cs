using System.Collections.Generic;
using System.Threading.Tasks;
using music.Domain.Common;
using music.Domain.Entities;

namespace music.Domain.Repositories
{
    public interface IPhotoRepository : IBaseRepository<Photo>
    {
        Task AddPhoto(Photo photo) ;   
        Task<Photo> GetPhotoByUserId(string Id) ; 
        Task<IEnumerable<Photo>> GetPhotosByMusicId(string Id) ; 
        Task<IEnumerable<Photo>> GetPhotoByAlbumId(string Id) ; 
    }
}