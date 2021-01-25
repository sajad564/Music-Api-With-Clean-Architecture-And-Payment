namespace music.Api.Dtos
{
    public class CartItemDto
    {
        public string Id {get;set;}
        public string CartId {get;set;}
        public MusicDto Music {get;set;}
        public AlbumDto Album {get;set;}
    }
}