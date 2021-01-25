using music.Domain.Entities;

namespace music.Api.Dtos
{
    public class GetMusicFileDto
    {
        public QualityEnum Quality {get;set;}
        public string Musicid {get;set;}
    }
}