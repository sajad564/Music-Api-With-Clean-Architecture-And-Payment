using System;

namespace music.Api.Dtos
{
    public class MusicDto
    {
        public string Name {get;set;}
        public bool Isfree {get;set;}
        public string Playtime {get;set;}
        public DateTime Publisheddate {get;set;}
        public UserDto Signer {get;set;}
        public IEquatable<PhotoDto> Photos {get;set;}
        public GradeDto Grade {get;set;}
    }
}