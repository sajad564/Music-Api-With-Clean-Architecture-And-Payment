using AutoMapper;
using music.Api.Dtos;
using music.Domain.Entities;

namespace music.Api.Profiles
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<AddAlbumDto,Album>();  
        }
    }
}