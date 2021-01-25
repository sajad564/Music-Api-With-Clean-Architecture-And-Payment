using AutoMapper;
using music.Api.Dtos;
using music.Domain.Entities;

namespace music.Api.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order,OrderDto>() ;
        }
    }
}