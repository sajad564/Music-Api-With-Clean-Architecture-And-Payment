using Microsoft.AspNetCore.Http;

namespace music.Api.Dtos
{
    public class AddUserPhotoDto
    {
        public string Userid {get;set;}
        public IFormFile Photo {get;set;}
    }
}