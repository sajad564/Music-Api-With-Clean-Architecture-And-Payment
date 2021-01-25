using AutoMapper;
using music.Api.Dtos;
using music.Domain.Entities;

namespace music.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserDto,User>();
        }
    }
}