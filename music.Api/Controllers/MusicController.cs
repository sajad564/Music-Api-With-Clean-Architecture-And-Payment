using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using music.Api.Common;
using music.Api.Dtos;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.PaginationParams;
using music.Domain.Services.Internal;

namespace music.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService musicService;
        private readonly IMapper mapper;
        public MusicController(IMapper mapper, IMusicService musicService)
        {
            this.mapper = mapper;
            this.musicService = musicService;

        }
        [Authorize(Roles = "signer,admin")]
        [HttpPost]
        public async Task<Response<bool>> AddMusic(AddMusicDto dto)
        {
            var newMusic = mapper.Map<Music>(dto) ; 
            var result = await musicService.AddMusicAsync(newMusic) ; 
            if(result.HaveError)
                return CustomResponse.Fail(result.Message , StatusCodeEnum.BADREQUEST) ;
        
            return CustomResponse.Ok() ; 
        }
        [HttpGet("{Id}")]
        public async Task<Response<MusicDto>> GetMusic(string Id)
        {
            var result = await musicService.GetMusicByIdAsync(Id);
            if (result.HaveError)
                return CustomResponse.Fail<MusicDto>(result.Message, StatusCodeEnum.BADREQUEST);

            var musicDto = mapper.Map<MusicDto>(result.item) ; 
            return CustomResponse.Ok<MusicDto>(musicDto) ; 
        
        }
        [Authorize(Roles = "signer,admin")]
        [HttpDelete("{Id}")]
        public async Task<Response<bool>> RemoveMusic(string Id)
        {
            var result = await musicService.RemoveMusicAsync(Id) ;
            if(result.HaveError)
                return CustomResponse.Fail(result.Message , StatusCodeEnum.BADREQUEST) ; 
        
            return CustomResponse.Ok() ; 
        }
        [HttpGet("page")]
        public IEnumerable<MusicDto> Page([FromQuery] MusicPaginationParams musicPaginationParams )
        {
            var result =  musicService.GetMusicPerPage(musicPaginationParams) ; 
            var pagination = mapper.Map<Pagination>(result) ;
            Response.AddPaginationToHeader(pagination) ;  
            var musics = mapper.Map<IEnumerable<MusicDto>>(result) ;  
            return musics ; 
        }
    }
}