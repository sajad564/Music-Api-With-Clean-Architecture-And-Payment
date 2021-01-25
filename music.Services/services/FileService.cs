
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using music.Domain;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.Services;
using music.Domain.Services.Internal;

namespace music.Services.services
{
    public class FileService : BaseService, IFileService
    {
        private readonly IUnitOfWork uow;
        private readonly IErrorMessages ErrorMessages;
        private readonly IFileManager fileManager;
        public FileService(IErrorMessages ErrorMessages, IUnitOfWork uow, IFileManager fileManager, IHttpContextAccessor accessor) : base(accessor)
        {
            this.fileManager = fileManager;
            this.ErrorMessages = ErrorMessages;
            this.uow = uow;

        }

        public async Task<Error> AddFileAsync(IFormFile file, AddFile info)
        {
            if (FileAlreadyExist(info))
                return Error.ToError(ErrorMessages.FileQualityExist);
            var parentState = albumOrMusicState(info.ParentId, info.Type);
            if (!(parentState.CurrentUserIsSigner || IsInRole("admin")))
                return Error.ToError(ErrorMessages.NotAuthorized);

            var uploadedData = await fileManager.AddFileAsync(file, info.Type);
            if (uploadedData == null)
                return Error.ToError(ErrorMessages.FileTypeIsNotValid);
            var newFile = new File
            {
                ContentType = uploadedData.ContentType , 
                MusicId = info.Type == FileTypeEnum.MUSIC ? info.ParentId : null,
                AlbumId = info.Type == FileTypeEnum.ZIP ? info.ParentId : null,
                Url = uploadedData.Url,
                Path = uploadedData.Path,
                Size = (double)file.Length / (1000 * 1000),
                CreatedDateTime = DateTime.Now,
                Type = info.Type
            };
            await uow.FileRepo.AddAsync(newFile);
            var transactionResult = await uow.SaveChangesAsync();
            if (!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail);

            return Error.WithoutError();
        }

        public Task<Error<File>> GetFileByAlbumIdAsync(string albumId, QualityEnum quality)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Error<File>> GetFileByIdAsync(string fileId)
        {
            var file = await uow.FileRepo.GetByIdAsync(fileId);
            var musicOrAlbumId = file.Type == FileTypeEnum.ZIP ? file.AlbumId : file.MusicId;
            // how is parent ? : music or album 
            var parentState = albumOrMusicState(musicOrAlbumId, file.Type);
            if (IsInRole("admin"))
                return Error<File>.WithoutError(file);
            else if (parentState.CurrentUserIsSigner)
                return Error<File>.WithoutError(file);
            else if (parentState.isFree)
                return Error<File>.WithoutError(file);

            return Error<File>.ToError(ErrorMessages.NotAuthorized);
        }

        public async Task<Error<File>> GetFileByMusicIdAsync(string musicId, QualityEnum quality)
        {
            var file = await uow.FileRepo.GetFileByMusicId(musicId, quality);
            if (IsInRole("admin"))
                return Error<File>.WithoutError(file);
            var parentState = albumOrMusicState(file.MusicId, file.Type);
            if (!(parentState.isFree || parentState.CurrentUserIsSigner))
                return Error<File>.ToError(ErrorMessages.NotAuthorized);

            return Error<File>.WithoutError(file);
        }

        public async Task<Error<BaseFile>> GetFileByUrl(string Url)
        {
            var authorize = await AuthorizeAccessFile(Url) ; 
            if(!authorize.Valid)
                return Error<BaseFile>.ToError(ErrorMessages.NotAuthorized) ;
            
            var file = authorize.file ; 
            var fileData = new BaseFile 
            {
                ContentType = file.ContentType ,
                Url = file.Url ,
                Path = file.Path 
            } ; 
            
            return Error<BaseFile>.WithoutError(fileData) ; 
        }

        public async Task<Error> RemoveFileById(string Id)
        {
            var music = await uow.MusicRepo.FindByExpression(m => m.Files.Where(f => f.Id==Id).Any()).FirstOrDefaultAsync() ;
            if(music==null)
                return Error.ToError(ErrorMessages.NotFound) ; 

            var musicSignerId = music.SignerId ; 
            var authorize = musicSignerId==UserId() || IsInRole("admin") ; 
            if(!authorize)
                return Error.ToError(ErrorMessages.NotAuthorized) ;
            
            var file = await uow.FileRepo.GetByIdAsync(Id) ;
            uow.FileRepo.Remove(file) ; 
            var transactionResult = await uow.SaveChangesAsync() ; 
            if(!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail);
            
            return Error.WithoutError() ;
        }
        private (bool CurrentUserIsSigner, bool isFree) albumOrMusicState(string albumOrMusicId, FileTypeEnum type)
        {
            if (type == FileTypeEnum.ZIP)
            {
                var findAlbum = uow.AlbumRepo.FindByExpression(album => album.Id == albumOrMusicId && album.SignerId == UserId()).FirstOrDefault();
                var isSigner = findAlbum.SignerId == UserId();
                return (isSigner, findAlbum.IsFree);
            }
            else
            {
                var findMusic = uow.MusicRepo.FindByExpression(music => music.Id == albumOrMusicId && music.SignerId == UserId()).FirstOrDefault();
                var isSigner = findMusic.SignerId == UserId();
                return (isSigner, findMusic.IsFree);
            }


        }
        private bool FileAlreadyExist(AddFile info)
        {
            return uow.FileRepo.FindByExpression(f =>
            (f.AlbumId == info.ParentId && f.Quality == info.Quality) ||
            (f.MusicId == info.ParentId && f.Quality == info.Quality)
             ).Any();
        }
        private async Task<( bool Valid , File file)> AuthorizeAccessFile(string Url)
        {
            var file = await uow.FileRepo.FindByExpression(f => f.Url==Url).FirstOrDefaultAsync() ;
            if(file==null)
                return (false , file) ;
            
            if(!string.IsNullOrEmpty(file.MusicId))
            {
                return (await AuthorizeAccessMusicOrAlbum(file.MusicId , FileTypeEnum.MUSIC) , file) ;
            }
            else
            {
                return (await AuthorizeAccessMusicOrAlbum(file.AlbumId , FileTypeEnum.ZIP) , file) ;
            }
        }
        private async Task<bool> AuthorizeAccessMusicOrAlbum(string parentId , FileTypeEnum type)
        {
            if(type==FileTypeEnum.MUSIC)
            {
                var music = await uow.MusicRepo.FindByExpression(m => m.Id==parentId).FirstOrDefaultAsync() ;
                if(music.IsFree && music!=null)
                    return true ;

                var order = await uow.OrderRepo.FindByExpression(forder => forder.UserId==UserId() && forder.MusicId==parentId).FirstOrDefaultAsync(); 
                if(order!=null)
                    return true ; 
                
                if(IsInRole("admin"))
                    return true ;
                
                return false ;
            }
            else 
            {
                var album = await uow.AlbumRepo.FindByExpression(m => m.Id==parentId).FirstOrDefaultAsync() ;
                if(album.IsFree && album!=null)
                    return true ;

                var order = await uow.OrderRepo.FindByExpression(forder => forder.UserId==UserId() && forder.albumId==parentId).FirstOrDefaultAsync(); 
                if(order!=null)
                    return true ; 
                
                if(IsInRole("admin"))
                    return true ;
                
                return false ;
            }
         }

    }
}