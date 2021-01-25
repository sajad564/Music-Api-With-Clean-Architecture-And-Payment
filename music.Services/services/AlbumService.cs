using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using music.Domain;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.Services.Internal;

namespace music.Services.services
{
    public class AlbumService : BaseService, IAlbumService
    {
        private readonly IUnitOfWork uow;
        private readonly IErrorMessages ErrorMessages;
        public AlbumService(IUnitOfWork uow, IHttpContextAccessor context, IErrorMessages ErrorMessages) : base(context)
        {
            this.ErrorMessages = ErrorMessages;
            this.uow = uow;

        }
        public async Task<Error> AddAlbumAsync(Album newAlbum)
        {
            if (!IsInRole("signer"))
                return Error.ToError(ErrorMessages.NotAuthorized);

            newAlbum.SignerId = UserId();
            await uow.AlbumRepo.AddAsync(newAlbum);
            var transactionResult = await uow.SaveChangesAsync();
            if (!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail) ; 

            return Error.WithoutError() ; 
        }

        public async Task<Error<Album>> GetAlbumByIdAsync(string AlbumId)
        {
            var findAlbum = await uow.AlbumRepo.GetAlbumById(AlbumId) ; 
            if(findAlbum==null)
                return Error<Album>.ToError(ErrorMessages.NotFound) ; 

            return Error<Album>.WithoutError(findAlbum) ; 
        }

        public async Task<Error> RemoveAlbumAsync(string albumId)
        {
            var findAlbum = await uow.AlbumRepo.GetAlbumById(albumId) ;
            if(findAlbum==null)
                return Error.ToError(ErrorMessages.NotFound) ; 

             uow.AlbumRepo.Remove(findAlbum) ; 
            bool transactionResult = await uow.SaveChangesAsync() ; 
            if(!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail) ; 

            return Error.WithoutError() ; 
        }
    }
}