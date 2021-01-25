using music.Domain.Common;

namespace music.Domain.Entities
{
    public class File : BaseEntity
    {
        public QualityEnum Quality {get;set;}
        public string AlbumId {get;set;}
        public string MusicId {get;set;}
        public Album Album {get;set;}
        public Music Music {get;set;}
        public FileTypeEnum Type {get;set;}
        public double Size {get;set;} //per meg
        public string Path {get;set;}
        public string Url {get;set;}
        public string ContentType {get;set;}
    }
}