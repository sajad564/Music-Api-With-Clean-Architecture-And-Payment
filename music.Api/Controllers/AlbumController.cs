using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using music.Api.Common;
using music.Api.Dtos;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.Services.Internal;

namespace music.Api.Controllers
{
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService albumService;
        private readonly IMapper mapper;
        public AlbumController(IMapper mapper, IAlbumService albumService)
        {
            this.mapper = mapper;
            this.albumService = albumService;

        }
        [Authorize(Roles="signer,admin")]
        [HttpPost("")]
        public async Task<Response<bool>> AddAlbum(AddAlbumDto dto)
        {
            var newAlbum = mapper.Map<Album>(dto) ; 
            var result = await albumService.AddAlbumAsync(newAlbum) ; 
            if(result.HaveError)
                return CustomResponse.Fail(result.Message , StatusCodeEnum.BADREQUEST) ;

            return CustomResponse.Ok() ;
        }
        [Authorize(Roles="admin,signer")]
        [HttpDelete("{Id}")]
        public async Task<Response<bool>> RemoveAlbum(string Id)
        {
            var result = await albumService.RemoveAlbumAsync(Id) ; 
            if(result.HaveError)
                return CustomResponse.Fail(result.Message , StatusCodeEnum.BADREQUEST) ; 

            return CustomResponse.Ok() ; 
        }
        [HttpGet("{Id}")]
        public async Task<Response<bool>> GetAlbum(string Id)
        {
            var result = await albumService.GetAlbumByIdAsync(Id) ; 
            if(result.HaveError)
                return CustomResponse.Fail(result.Message , StatusCodeEnum.BADREQUEST) ; 

            return CustomResponse.Ok() ; 
        }
    }
}