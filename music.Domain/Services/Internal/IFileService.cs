using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using music.Domain.Common;
using music.Domain.Entities;

namespace music.Domain.Services.Internal
{
    public interface IFileService : IBaseService
    {
        Task<Error<File>> GetFileByMusicIdAsync(string musicId , QualityEnum quality ) ; 
        Task<Error<File>> GetFileByAlbumIdAsync(string albumId , QualityEnum quality) ; 
        Task<Error<File>> GetFileByIdAsync(string fileId) ;
        Task<Error> RemoveFileById(string Id) ; 
        Task<Error> AddFileAsync(IFormFile file ,AddFile info) ; 
        Task<Error<BaseFile>> GetFileByUrl(string Url) ; 
    }
}