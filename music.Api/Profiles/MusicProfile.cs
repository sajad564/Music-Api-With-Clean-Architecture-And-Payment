using AutoMapper;
using music.Api.Dtos;
using music.Domain.Entities;

namespace music.Api.Profiles
{
    public class MusicProfile : Profile
    {
        public MusicProfile()
        {
            CreateMap<AddMusicDto , Music>() ; 
            CreateMap<Music,MusicDto>() ; 
        }
    }
}