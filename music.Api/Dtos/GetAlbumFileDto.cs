using music.Domain.Entities;

namespace music.Api.Dtos
{
    public class GetAlbumFileDto
    {
        public QualityEnum Quality {get;set;}
        public string Albumid {get;set;}
    }
}