using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using music.Api.Common;
using music.Api.Dtos;
using music.Domain.Common;
using music.Domain.Services.Internal;

namespace music.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService photoService;
        public PhotoController(IPhotoService photoService)
        {
            this.photoService = photoService;

        }
        [HttpPost("[Action]")]
        public async Task<Response<bool>> User([FromBody] AddUserPhotoDto dto)
        {
            var result = await photoService.AddUserPhoto(dto.Photo , dto.Userid) ; 
            if(result.HaveError)
                return CustomResponse.Fail<bool>(result.Message ,StatusCodeEnum.BADREQUEST) ; 
            return CustomResponse.Ok() ; 
        }
        [HttpPost("[Action]")]
        public async Task<Response<bool>> Music([FromBody] AddMusicPhotosDto dto)
        {
            var result = await photoService.AddMusicPhotos(dto.Photos, dto.Musicid) ; 
            if(result.HaveError)
                return CustomResponse.Fail(result.Message , StatusCodeEnum.BADREQUEST) ; 

            return CustomResponse.Ok() ; 
        }
        public async Task<Response<bool>> Album([FromBody] AddAlbumPhotosDto dto)
        {
            var result = await photoService.AddMusicPhotos(dto.Photos , dto.Albumid) ;

            if(result.HaveError)
                return CustomResponse.Fail(result.Message ,StatusCodeEnum.BADREQUEST) ; 

            return CustomResponse.Ok() ; 

        }
    }
}