using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using music.Domain.Services.Internal;

namespace music.Api.Controllers
{
    [ApiController]
    [Route("static")]
    public class StaticController : ControllerBase
    {
        private readonly IPhotoService photoService;
        private readonly IFileService fileService;
        public StaticController(IPhotoService photoService, IFileService fileService)
        {
            this.fileService = fileService;
            this.photoService = photoService;

        }
        [Authorize]
        [HttpGet("MUSIC")]
        public async Task<IActionResult> Music()
        {
            var url = HttpContext.Request.Path;
            var result = await fileService.GetFileByUrl(url) ; 
            if(result.HaveError)
                return Unauthorized();
            return PhysicalFile(result.item.Path , result.item.ContentType) ; 
        }
        [Authorize]
        [HttpGet("ALBUM")]
        public async Task<IActionResult> Album()
        {
            var url = HttpContext.Request.Path;
            var result = await fileService.GetFileByUrl(url) ; 
            if(result.HaveError)
                return Unauthorized();

            return PhysicalFile(result.item.Path , result.item.ContentType) ; 
        }
    }
}