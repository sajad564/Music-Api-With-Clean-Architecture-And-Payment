using System.Collections.Generic;

namespace music.Api.Dtos
{
    public class AlbumDto
    {
        public string Id {get;set;}
        public string Name {get;set;}
        public int Count {get;set;}
        public bool Isfree {get;set;}
        public UserDto Signer {get;set;}
        public GradeDto Grade {get;set;}
        public IEnumerable<MusicDto> musics {get;set;}
        public IEnumerable<PhotoDto> Photo {get;set;}
    }
}