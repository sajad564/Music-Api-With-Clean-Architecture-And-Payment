using music.Domain.Common;
using music.Domain.Entities;

namespace music.Domain.PaginationParams
{
    public class AlbumPaginationParams : BasePaginationParams
    {
        public string SignerId {get;set;}
        public string Name {get;set;}
        public bool IsFree {get;set;}
        public QualityEnum Quality {get;set;}
    }
}