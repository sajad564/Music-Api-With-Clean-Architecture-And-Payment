using Microsoft.AspNetCore.Http;
using music.Domain.Entities;

namespace music.Api.Dtos
{
    public class AddFileDto 
    {
        public QualityEnum Quality {get;set;}
        public IFormFile file {get;set;}
        public FileTypeEnum type {get;set;}
        public string Parentid {get;set;}
        
    }
}