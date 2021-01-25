namespace music.Domain.Entities
{
    public class AddFile
    {
        public FileTypeEnum Type {get;set;}
        public QualityEnum Quality {get;set;}
        public string ParentId {get;set;}
    }
}