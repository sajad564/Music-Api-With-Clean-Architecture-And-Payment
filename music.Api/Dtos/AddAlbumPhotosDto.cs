using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace music.Api.Dtos
{
    public class AddAlbumPhotosDto
    {
        public string Albumid {get;set;}
        public IEnumerable<IFormFile> Photos {get;set;}
    }
}