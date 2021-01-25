using music.Domain.Common;

namespace music.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string MusicId {get;set;}
        public string albumId {get;set;}
        public string UserId {get;set;}
        public Music Music {get;set;}
        public Album Album {get;set;}
        public User User {get;set;}
    }
}