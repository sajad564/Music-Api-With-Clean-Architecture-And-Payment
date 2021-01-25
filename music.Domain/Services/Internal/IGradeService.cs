using System.Threading.Tasks;
using music.Domain.Common;

namespace music.Domain.Services.Internal
{
    public interface IGradeService  : IBaseService
    {
        Task<Error> AddMusicGrade(string musicId , double newScore ) ;  
        Task<Error> AddAlbumGrade(string albumId , double newScore) ; 
        Task<Error> AddSignerGrade(string signerId ,double newScore) ; 
    }
}