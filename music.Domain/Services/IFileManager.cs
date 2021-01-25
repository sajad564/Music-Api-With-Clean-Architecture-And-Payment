using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using music.Domain.Common;
using music.Domain.Entities;

namespace music.Domain.Services
{
    public interface IFileManager
    {
         bool DeleteFile(string source) ;
         Task<BaseFile> AddFileAsync(IFormFile file,FileTypeEnum type) ;
         Task<IEnumerable<BaseFile>> AddMultipleFileAsync(IEnumerable<IFormFile> files,FileTypeEnum type)  ; 
    }
}