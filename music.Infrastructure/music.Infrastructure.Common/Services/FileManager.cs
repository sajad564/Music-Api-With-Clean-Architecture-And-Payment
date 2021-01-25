using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.Services;
using music.Infrastructure.Common.Common;

namespace music.Infrastructure.Common.Services
{
    public class FileManager : IFileManager
    {
        private string wwwRoot =  Path.GetDirectoryName(Environment.CurrentDirectory) + "/wwwroot" ; 

        private readonly PathConfiguration PathConf;
        public FileManager(PathConfiguration PathConf)
        {
            this.PathConf = PathConf;

        }
        public async Task<BaseFile> AddFileAsync(IFormFile file, FileTypeEnum type)
        {
            if(!ValidateFileType(file.FileName , type))
                return null ;


           var FolderPath = GeneratePathByFileType(type) ;
            
            var uploadedFileData  = await UploadAsync(file,FolderPath) ;
            return uploadedFileData ; 
        }

        public async Task<IEnumerable<BaseFile>> AddMultipleFileAsync(IEnumerable<IFormFile> files, FileTypeEnum type)
        {
            List<BaseFile> data = new List<BaseFile>() ; 
             foreach (var file in files)
            {
                if(!ValidateFileType(file.FileName , type))
                    return null ;
                    
                var uploadedfiledata = await AddFileAsync(file , type) ;
                data.Add(uploadedfiledata) ;
            }
            return data ;
        }

        public bool DeleteFile(string source)
        {
            try 
            {
                 System.IO.File.Delete(source);
            }
            catch 
            {
                return false ;
            }
            return true ;
        }
         private async Task<BaseFile> UploadAsync(IFormFile file , string FolderPath ) {
            
            string UniqueFileName = Guid.NewGuid() + file.FileName.Replace('/','_') ;
             string fileFullPath = $"{FolderPath}/{UniqueFileName}" ; 
             using(var stream = new FileStream(fileFullPath , FileMode.Create , FileAccess.Write)) {
                 await file.CopyToAsync(stream) ; 
             }
             var uploadFileData = new BaseFile {
                 ContentType = file.ContentType  , 
                 Path  = fileFullPath , 
                 Url = GenerateUrlByPath(fileFullPath)  
             } ;
             return uploadFileData ;  
        }
        private string GenerateUrlByPath(string path)
        {
            if(path.Contains(PathConf.BaseDownloadPath))
                return path.Replace(PathConf.BaseDownloadPath , PathConf.BaseDownloadUrl + "/static") ; 

            return path.Replace(wwwRoot , PathConf.BaseDownloadUrl) ; 
        }
        private bool ValidateFileType(string FileName , FileTypeEnum type)
        {
            return ValidExtensionsPerFileType(type).Where(ftype => ftype==Path.GetExtension(FileName)).Any()   ; 
        }
        private string[] ValidExtensionsPerFileType(FileTypeEnum type)
        {
            
            switch (type)
            {
                case FileTypeEnum.MUSIC :
                        return new string[] {".mp3"} ;
                case FileTypeEnum.ZIP :
                        return new string[] {".zip"} ; 
                case FileTypeEnum.PHOTO :
                        return new string[] {".jpg" , ".png" , ".svg"} ; 
                default :
                    return new string[] {} ;
            }
        }
        private string GeneratePathByFileType(FileTypeEnum type) 
        {
            string fileFolderPerType = null ;  
            switch(type) {
                case FileTypeEnum.PHOTO : 
                    fileFolderPerType = $"{wwwRoot}/PHOTO" ; 
                    break ;
                case FileTypeEnum.MUSIC :
                    fileFolderPerType = $"{PathConf.BaseDownloadPath}/MUSIC" ; 
                    break ;
                case FileTypeEnum.ZIP : 
                    fileFolderPerType = $"{PathConf.BaseDownloadPath}/ALBUM" ;
                    break ;  
            }
            CreateDirectoryIfDoesNotExist(fileFolderPerType) ;
            return fileFolderPerType ;  
        }
        private void CreateDirectoryIfDoesNotExist(string directory) 
        {
            if(!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory) ; 
            }
        }
    }
}