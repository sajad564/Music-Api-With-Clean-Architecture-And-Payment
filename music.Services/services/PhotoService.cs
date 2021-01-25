using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using music.Domain;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.Services;
using music.Domain.Services.Internal;
using music.Services.Common;

namespace music.Services.services
{
    public class PhotoService : BaseService, IPhotoService
    {
        private readonly IUnitOfWork uow;
        private readonly IErrorMessages ErrorMessages;
        private readonly IFileManager fileManager;
        public PhotoService(IFileManager fileManager, IErrorMessages ErrorMessages, IHttpContextAccessor accessor, IUnitOfWork uow) : base(accessor)
        {
            this.fileManager = fileManager;
            this.ErrorMessages = ErrorMessages;
            this.uow = uow;

        }

        public async Task<Error<IEnumerable<Photo>>> GetPhotoByAlbumIdAsync(string albumId)
        {
            var photos = await uow.PhotoRepo.GetPhotoByAlbumId(albumId);
            if (!photos.Any())
                return Error<IEnumerable<Photo>>.ToError(ErrorMessages.NotFound);

            return Error<IEnumerable<Photo>>.WithoutError(photos);
        }

        public async Task<Error<Photo>> GetPhotoByUserIdAsync(string userId)
        {
            var photo = await uow.PhotoRepo.GetPhotoByUserId(userId);
            if (photo == null)
                return Error<Photo>.ToError(ErrorMessages.NotFound);

            return Error<Photo>.WithoutError(photo);
        }

        public async Task<Error<IEnumerable<Photo>>> GetPhotoByMusicIdAsync(string musicId)
        {
            var photos = await uow.PhotoRepo.GetPhotosByMusicId(musicId);
            if (!photos.Any())
                return Error<IEnumerable<Photo>>.ToError(ErrorMessages.NotFound);

            return Error<IEnumerable<Photo>>.WithoutError(photos);
        }

        public async Task<Error> AddUserPhoto(IFormFile photo, string userId)
        {
            if (!IsInRole("admin") || !(!string.IsNullOrEmpty(UserId()) && UserId() == userId))
                return Error.ToError(ErrorMessages.NotAuthorized);

            var uploadedData = await fileManager.AddFileAsync(photo , FileTypeEnum.PHOTO) ; 
            if(uploadedData==null)
                return Error.ToError(ErrorMessages.FileTypeIsNotValid) ; 
            var userPhoto = await uow.PhotoRepo.GetPhotoByUserId(UserId()) ; 
            if(!string.IsNullOrEmpty(userPhoto.Path))
                fileManager.DeleteFile(userPhoto.Path) ;
            userPhoto.Path = uploadedData.Path ;
            userPhoto.Url = uploadedData.Url ;
            var transactionResult = await uow.SaveChangesAsync() ; 
            if(!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail) ; 

            return Error.WithoutError() ;     
        }

        public async Task<Error> AddAlbumPhotos(IEnumerable<IFormFile> photos, string albumId)
        {
            if(!(await AuthorizeUserForEditPhoto(albumId , PhotoTypeEnum.ALBUMPHOTO)))
                return Error.ToError(ErrorMessages.NotAuthorized) ; 

            var succeeded = await AddMultipleFilesAsync(photos, albumId , PhotoTypeEnum.ALBUMPHOTO) ; 
            if(!succeeded)
                return Error.ToError(ErrorMessages.FileTypeIsNotValid) ; 

            var transactionResult = await uow.SaveChangesAsync() ; 
            if(!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail) ; 
            
            return Error.WithoutError() ; 
        }

        public async Task<Error> AddMusicPhotos(IEnumerable<IFormFile> photos, string musicId)
        {
            if(!(await AuthorizeUserForEditPhoto(musicId ,PhotoTypeEnum.MUSICPHOTO)))
                return Error.ToError(ErrorMessages.NotAuthorized) ; 
            var succeeded = await AddMultipleFilesAsync(photos ,musicId , PhotoTypeEnum.MUSICPHOTO ) ;
            if(!succeeded)
                return Error.ToError(ErrorMessages.NotAuthorized) ; 
            var transactionResult = await uow.SaveChangesAsync() ; 
            if(!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail) ; 
            
            return Error.WithoutError() ; 
        }
        private async Task<bool> AddMultipleFilesAsync(IEnumerable<IFormFile> photos , string parentId , PhotoTypeEnum photoType)
        {
            List<string> uploadedPath = new List<string>();
            foreach(var photo in photos)
            {
                var uploadedFileData = await fileManager.AddFileAsync(photo , FileTypeEnum.PHOTO) ; 
                if(uploadedFileData==null)
                {
                    RemoveUploadedFiles(uploadedPath) ;
                    return false ;  
                }
                else
                {
                    uploadedPath.Add(uploadedFileData.Path) ; 
                    var newPhoto = new Photo
                    {
                        Url = uploadedFileData.Url , 
                        Path = uploadedFileData.Path ,
                        AlbumId = photoType==PhotoTypeEnum.ALBUMPHOTO ? parentId : null ,
                        MusicId = photoType==PhotoTypeEnum.MUSICPHOTO ? parentId : null 
                    } ; 
                    await uow.PhotoRepo.AddAsync(newPhoto) ; 
                }

            }
            return true ; 
        }
        private async Task<bool> AuthorizeUserForEditPhoto(string parentId , PhotoTypeEnum photoType)
        {
            if(photoType==PhotoTypeEnum.ALBUMPHOTO)
            {
                var albumId = parentId ; 
                var findAlbum = await uow.AlbumRepo.GetByIdAsync(albumId) ;
                return IsInRole("admin") || (IsInRole("signer") && findAlbum.SignerId==UserId())  ; 
            }
            else 
            {
                var musicId = parentId ; 
                var findMusic = await uow.MusicRepo.GetByIdAsync(musicId) ; 
                return IsInRole("admin") || (IsInRole("signer") && findMusic.SignerId==UserId()) ; 
            }
        }
        private void RemoveUploadedFiles(IEnumerable<string> sources)
        {
            foreach(var source in sources)
                fileManager.DeleteFile(source) ; 
        }
    }
}