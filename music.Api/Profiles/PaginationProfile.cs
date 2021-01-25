using AutoMapper;
using music.Api.Common;
using music.Domain.Common;

namespace music.Api.Profiles
{
    public class PaginationProfile : Profile
    {
        public PaginationProfile()
        {
            CreateMap(typeof(PagedList<>) , typeof(Pagination))  ;
        }
    }
}