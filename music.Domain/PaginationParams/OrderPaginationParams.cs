using music.Domain.Common;

namespace music.Domain.PaginationParams
{
    public class OrderPaginationParams : BasePaginationParams
    {
        public string UserId {get;set;}
        public string albumId {get;set;}
        public string musicId {get;set;}
    }
}