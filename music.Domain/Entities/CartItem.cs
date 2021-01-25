using music.Domain.Common;

namespace music.Domain.Entities
{
    public class CartItem : BaseEntity
    {
        public string CartId {get;set;}
        public string MusicId {get;set;}
        public string AlbumId {get;set;}
        public Cart Cart {get;set;}
        public Music Music {get;set;}
        public Album Album {get;set;}
    }
}