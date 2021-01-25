namespace music.Domain.Common
{
    public class BaseFile : BaseEntity
    {
        public string ContentType {get;set;}
        // on system..
        public string Path {get;set;}
        // on web..
        public string Url {get;set;}
    }
}