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
    [Route("api/[Controller]")]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly IFileService fileService;
        private readonly IMapper mapper;
        public FileController(IMapper mapper, IFileService fileService)
        {
            this.mapper = mapper;
            this.fileService = fileService;

        }
        [HttpPost("[Action]")]
        public async Task<Response<bool>> Music([FromBody] AddFileDto dto)
        { 
            dto.type= FileTypeEnum.MUSIC ; 
            var addFile = mapper.Map<AddFile>(dto) ; 
            var result = await fileService.AddFileAsync(dto.file , addFile) ; 
            if(result.HaveError)
                return CustomResponse.Fail(result.Message,StatusCodeEnum.BADREQUEST); 
            return CustomResponse.Ok() ; 
        }
        [HttpPost("[Action]")]
        public async Task<Response<bool>> Album([FromBody] AddFileDto dto)
        { 
            dto.type= FileTypeEnum.ZIP ; 
            var addFile = mapper.Map<AddFile>(dto) ; 
            var result = await fileService.AddFileAsync(dto.file , addFile) ; 
            if(result.HaveError)
                return CustomResponse.Fail(result.Message,StatusCodeEnum.BADREQUEST); 
            return CustomResponse.Ok() ; 
        }
        [HttpGet("[Action]/{Id}")]
        public async Task<Response<FileDto>> Music([FromQuery] GetMusicFileDto dto)
        {
            var result = await fileService.GetFileByMusicIdAsync(dto.Musicid  , dto.Quality) ;  
            if(result.HaveError)
                return CustomResponse.Fail<FileDto>(result.Message , StatusCodeEnum.BADREQUEST) ; 

            var fileDto = mapper.Map<FileDto>(result.item) ;  
            return CustomResponse.Ok<FileDto>(fileDto) ; 
        }

        [HttpGet("[Action]/{Id}")]
        public async Task<Response<FileDto>> Album([FromQuery] GetAlbumFileDto dto)
        {
            var result = await fileService.GetFileByMusicIdAsync(dto.Albumid  , dto.Quality) ;  
            if(result.HaveError)
                return CustomResponse.Fail<FileDto>(result.Message , StatusCodeEnum.BADREQUEST) ; 

            var fileDto = mapper.Map<FileDto>(result.item) ;  
            return CustomResponse.Ok<FileDto>(fileDto) ; 
        }
        [HttpDelete("{Id}")]
        public async Task<Response<bool>> RemoveMusicFile(string Id)
        {
            var result = await fileService.RemoveFileById(Id) ; 
            if(result.HaveError)
                return CustomResponse.Fail(result.Message , StatusCodeEnum.BADREQUEST) ; 

            return CustomResponse.Ok() ; 
            
        }
    }
}