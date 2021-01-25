using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using music.Api.Common;
using music.Api.Dtos;
using music.Domain.Common;
using music.Domain.Services.Internal;

namespace music.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService gradeService;
        private readonly IMapper mapper;
        public GradeController(IGradeService gradeService, IMapper mapper)
        {
            this.mapper = mapper;
            this.gradeService = gradeService;

        }
        [HttpPost("[Action]")]
        public async Task<Response<bool>> Music([FromBody] AddMusicGradeDto dto)
        {
            var result = await gradeService.AddMusicGrade(dto.musicId ,dto.Score) ; 
            if(result.HaveError)
                return CustomResponse.Fail(result.Message , StatusCodeEnum.BADREQUEST) ;
        
            return CustomResponse.Ok() ; 
        }
        [HttpPost("[Action]")]
        public async Task<Response<bool>> Album([FromBody] AddAlbumGradeDto dto)
        {
            var result = await gradeService.AddAlbumGrade(dto.Albumid ,dto.Score) ; 
            if(result.HaveError)
                return CustomResponse.Fail(result.Message , StatusCodeEnum.BADREQUEST) ;
        
            return CustomResponse.Ok() ; 
        }
        [HttpPost("[Action]")]
        public async Task<Response<bool>> Signer([FromBody] AddSignerGradeDto dto)
        {
            var result = await gradeService.AddSignerGrade(dto.Signerid ,dto.Score) ; 
            if(result.HaveError)
                return CustomResponse.Fail(result.Message , StatusCodeEnum.BADREQUEST) ;
        
            return CustomResponse.Ok() ; 
        }
    }
}