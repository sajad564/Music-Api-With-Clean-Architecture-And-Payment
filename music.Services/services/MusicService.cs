using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using music.Domain;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.PaginationParams;
using music.Domain.Services.Internal;

namespace music.Services.services
{
    public class MusicService : BaseService, IMusicService
    {
        private readonly IUnitOfWork uow;
        private readonly IErrorMessages ErrorMessages;
        public MusicService(IErrorMessages ErrorMessages, IUnitOfWork uow, IHttpContextAccessor accessor) : base(accessor)
        {
            this.ErrorMessages = ErrorMessages;
            this.uow = uow;

        }
        public async Task<Error> AddMusicAsync(Music music)
        {
            if (!IsInRole("signer"))
                return Error.ToError(ErrorMessages.NotAuthorized) ; 
            music.SignerId = UserId(); 
            music.PublishedDate = DateTime.Now ;
            await uow.MusicRepo.AddAsync(music) ; 
            bool transactionResult = await uow.SaveChangesAsync() ;
            if(!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail) ; 

            return Error.WithoutError() ; 
        }

        public async Task<Error<Music>> GetMusicByIdAsync(string musicId)
        {
            var findMusic = await uow.MusicRepo.GetMusicById(musicId) ; 
            if(findMusic==null)
                return Error<Music>.ToError(ErrorMessages.NotFound) ;

            return Error<Music>.WithoutError(findMusic) ; 
        }

        public PagedList<Music> GetMusicPerPage(MusicPaginationParams paginationParams)
        {
            return  uow.MusicRepo.GetMusicPerPage(paginationParams) ;
        }

        public async Task<Error> RemoveMusicAsync(string MusicId)
        {
            var findMusic = await uow.MusicRepo.GetByIdAsync(MusicId) ; 
            
            if(findMusic==null)
                return Error.ToError(ErrorMessages.NotFound) ; 

            if(!IsInRole("admin") || findMusic.SignerId!=UserId())
                return Error.ToError(ErrorMessages.NotAuthorized) ; 
        
             uow.MusicRepo.Remove(findMusic) ; 
            bool transactionResult = await uow.SaveChangesAsync() ; 
            if(!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail) ; 
            
            return Error.WithoutError() ; 
        }

    }
}