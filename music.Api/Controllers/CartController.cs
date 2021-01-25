using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using music.Api.Common;
using music.Api.Dtos;
using music.Domain.Common;
using music.Domain.Services.Internal;

namespace music.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICartService cartService;
        public CartController(IMapper mapper, ICartService cartService)
        {
            this.cartService = cartService;
            this.mapper = mapper;

        }
        [HttpGet("{cartId}")]
        public async Task<Response<CartDto>> GetCart()
        {
            var cart =  await cartService.GetCurrenUserCart() ; 
            var cartDto = mapper.Map<CartDto>(cart) ;
            return CustomResponse.Ok<CartDto>(cartDto) ;
        }
        [HttpPost("[Action]")]
        public async Task<Response<bool>> AddMusic([FromBody] AddMusicToCartDto dto)
        {
            var result = await cartService.AddMusicToCart(dto.Musicid,dto.Cartid);
            if(result.HaveError)
                return CustomResponse.Fail(result.Message , StatusCodeEnum.BADREQUEST) ; 

            return CustomResponse.Ok() ; 
        }
        
        [HttpPost("[Action]")]
        public async Task<Response<bool>> AddAlbum([FromBody] AddAlbumToCartDto dto)
        {
            var result = await cartService.AddAlbumToCart(dto.Albumid , dto.Cartid) ; 
            if(result.HaveError)
                return CustomResponse.Fail(result.Message , StatusCodeEnum.BADREQUEST) ; 

            return CustomResponse.Ok() ; 
        }
        [HttpDelete("{Id}")]
        public async Task<Response<bool>> RemoveCartItem(string Id)
        {
            var result = await cartService.RemoveCartItem(Id) ; 
            if(result.HaveError)
                return CustomResponse.Fail(result.Message,StatusCodeEnum.BADREQUEST) ;

            return CustomResponse.Ok() ; 
        } 
    }
}