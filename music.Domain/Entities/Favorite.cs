using music.Domain.Common;

namespace music.Domain.Entities
{
    public class Favorite : BaseEntity
    {
        public string UserId {get;set;}
        public User User {get;set;}
        public string MusicId {get;set;}
        public string AlbumId {get;set;}
        public Music Music {get;set;}
        public Album Album {get;set;}
    }
}