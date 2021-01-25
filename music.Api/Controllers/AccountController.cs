using System.Threading.Tasks;
using AutoMapper;
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
    public class AccountController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        public AccountController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;

        }
        [HttpPost()]
        public async Task<Response<bool>> Register([FromBody] RegisterUserDto dto)
        {
            var nUser = mapper.Map<User>(dto);
            var result = await userService.AddUser(nUser ,dto.Password) ; 
            if(result.HaveError)
                return CustomResponse.Fail<bool>(result.Message , StatusCodeEnum.BADREQUEST) ; 

            return CustomResponse.Ok() ; 
        }
        [HttpPost("[Action]")]
        public async Task<Response<Token>> Login([FromBody] LoginDto dto )
        {
            var result = await userService.LoginUser(dto.Username, dto.Password); 
            if(result.HaveError)
                return CustomResponse.Fail<Token>(result.Message,StatusCodeEnum.UNAUTHORIZED) ; 
        
            return CustomResponse.Ok<Token>(result.item) ; 
        }
        public async Task<Response<Token>> Refresh([FromBody] Token token)
        {
            var result = await userService.Refresh(token);
            if(result.HaveError)
                return CustomResponse.Fail<Token>(result.Message , StatusCodeEnum.UNAUTHORIZED);

            return CustomResponse.Ok<Token>(result.item) ; 
        }
    }
}