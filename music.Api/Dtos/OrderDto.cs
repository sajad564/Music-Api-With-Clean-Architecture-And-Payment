using System;

namespace music.Api.Dtos
{
    public class OrderDto
    {
        public string Id {get;set;}
        public DateTime CreatedDateTime {get;set;}
        public MusicDto Music {get;set;}
        public AlbumDto Album {get;set;}
        public UserDto User {get;set;}
    }
}