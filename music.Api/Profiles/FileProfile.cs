using AutoMapper;
using music.Api.Dtos;
using music.Domain.Entities;

namespace music.Api.Profiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<AddFileDto,AddFile>() ;
            CreateMap<File,FileDto>() ; 
        }
    }
}