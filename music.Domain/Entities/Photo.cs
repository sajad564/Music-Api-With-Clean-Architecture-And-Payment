using music.Domain.Common;

namespace music.Domain.Entities
{
    public class Photo : BaseFile
    {
        public string UserId {get;set;}
        public string MusicId {get;set;}
        public string AlbumId {get;set;}
        public Album Album {get;set;}
        public Music Music {get;set;}
        public User User {get;set;}
    }
}