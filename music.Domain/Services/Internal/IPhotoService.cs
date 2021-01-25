using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using music.Domain.Common;
using music.Domain.Entities;

namespace music.Domain.Services.Internal
{
    public interface IPhotoService : IBaseService
    {
        Task<Error> AddUserPhoto(IFormFile photo , string userId) ; 
        Task<Error> AddAlbumPhotos(IEnumerable<IFormFile> photos , string albumId) ; 
        Task<Error> AddMusicPhotos(IEnumerable<IFormFile> photos , string musicId) ; 
        Task<Error<Photo>> GetPhotoByUserIdAsync(string userId) ; 
        Task<Error<IEnumerable<Photo>>> GetPhotoByAlbumIdAsync(string albumId) ;  
        Task<Error<IEnumerable<Photo>>> GetPhotoByMusicIdAsync(string musicId) ; 
    }
}