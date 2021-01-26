using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using music.Api.Common;
using music.Api.Dtos;
using music.Domain.Common;
using music.Domain.PaginationParams;
using music.Domain.Services.Internal;

namespace music.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IShopingService shopingService;
        public OrderController(IMapper mapper, IShopingService shopingService)
        {
            this.shopingService = shopingService;
            this.mapper = mapper;

        }
        [Authorize]
        [HttpGet("{Id}")]
        public async Task<Response<OrderDto>> GetOrderById(string Id)
        {
            var result = await shopingService.GetOrderById(Id) ; 
            if(result.HaveError)
                return CustomResponse.Fail<OrderDto>(result.Message, StatusCodeEnum.BADREQUEST) ; 

            var dto = mapper.Map<OrderDto>(result.item) ; 
            return CustomResponse.Ok<OrderDto>(dto) ; 
        }
        [HttpGet("[Action]")]
        public Response<IEnumerable<OrderDto>> Page([FromQuery] OrderPaginationParams paginationParams)
        {
            var result = shopingService.OrderPerPage(paginationParams) ; 
            if(result.HaveError)
                return CustomResponse.Fail<IEnumerable<OrderDto>>(result.Message,StatusCodeEnum.BADREQUEST) ; 

            var orderDto = mapper.Map<IEnumerable<OrderDto>>(result.item) ; 
            return CustomResponse.Ok<IEnumerable<OrderDto>>(orderDto) ; 
        }
        
    }
}