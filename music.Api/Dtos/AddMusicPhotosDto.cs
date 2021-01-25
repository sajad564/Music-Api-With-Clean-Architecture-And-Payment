using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace music.Api.Dtos
{
    public class AddMusicPhotosDto
    {
        public string Musicid {get;set;}
        public IEnumerable<IFormFile> Photos {get;set;}
    }
}